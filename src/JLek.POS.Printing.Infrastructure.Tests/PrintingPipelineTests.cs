using FluentAssertions;
using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Printing.Models;
using JLek.POS.Printing.Renderer;
using Xunit;

namespace JLek.POS.Printing.Infrastructure.Tests;

public sealed class PrintingPipelineTests
{
    private readonly RenderOptions _renderOptions = new()
    {
        CharactersPerLine = 48,
        EncodingName = "windows-874",
        CutPaper = true
    };

    private readonly PrinterConfiguration _printerConfig = new()
    {
        PaperWidthMm = 80,
        CharactersPerLine = 48,
        Encoding = "windows-874",
        DefaultAdapter = "usb",
        PrintTimeoutSeconds = 30
    };

    private ReceiptDocument CreateSampleDocument()
    {
        return new ReceiptDocument
        {
            Title = "JLek Shop",
            ReceiptNumber = "RC20260717001",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "ผัดไทยกุ้งสด", Alignment = ReceiptAlignment.Left, Bold = true },
                new() { Text = "  2x @ 50.00 = 100.00", Alignment = ReceiptAlignment.Left },
                new() { Text = "รวมทั้งสิ้น        100.00", Alignment = ReceiptAlignment.Right, Bold = true },
                new() { Text = "ขอบคุณที่ใช้บริการ", Alignment = ReceiptAlignment.Center }
            }
        };
    }

    // ── 1. Full Pipeline: Renderer → USB Adapter ──────────────

    [Fact]
    public async Task Pipeline_RendererToUsb_Should_PassPayloadUnchanged()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        var result = await adapter.PrintAsync(payload);

        // Assert
        result.Success.Should().BeTrue();
        port.OpenCallCount.Should().Be(1);
        port.CloseCallCount.Should().Be(1);
        port.WrittenData.ToArray().Should().BeEquivalentTo(payload.Data.ToArray());
        port.WrittenData.ToArray().Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Pipeline_RendererToLan_Should_PassPayloadUnchanged()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var client = new MockTcpClient();
        var adapter = new LanPrinterAdapter(client, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        var result = await adapter.PrintAsync(payload);

        // Assert
        result.Success.Should().BeTrue();
        client.ConnectCallCount.Should().Be(1);
        client.DisconnectCallCount.Should().Be(1);
        client.SentData.ToArray().Should().BeEquivalentTo(payload.Data.ToArray());
    }

    [Fact]
    public async Task Pipeline_RendererToUsb_PayloadBytes_Should_ContainReceiptData()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        await adapter.PrintAsync(payload);

        // Assert — ESC/POS bytes contain receipt content
        var bytes = port.WrittenData.ToArray();
        var text = System.Text.Encoding.UTF8.GetString(bytes);
        text.Should().Contain("JLek Shop");
        text.Should().Contain("RC20260717001");
    }

    // ── 2. Renderer is Stateless ──────────────────────────────

    [Fact]
    public async Task Renderer_Stateless_MultipleDocuments_Should_ProduceDifferentPayloads()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);

        var doc1 = new ReceiptDocument
        {
            Title = "Receipt A",
            Lines = new List<ReceiptLine>()
        };
        var doc2 = new ReceiptDocument
        {
            Title = "Receipt B",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload1 = await renderer.RenderAsync(doc1);
        var payload2 = await renderer.RenderAsync(doc2);

        // Assert — different documents produce different payloads
        payload1.Data.ToArray().Should().NotBeEquivalentTo(payload2.Data.ToArray());
    }

    [Fact]
    public async Task Renderer_Stateless_SameDocument_Twice_Should_ProduceIdenticalPayloads()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var doc = CreateSampleDocument();

        // Act
        var payload1 = await renderer.RenderAsync(doc);
        var payload2 = await renderer.RenderAsync(doc);

        // Assert
        payload1.Data.ToArray().Should().BeEquivalentTo(payload2.Data.ToArray());
    }

    // ── 3. Adapters Never Modify Payload ───────────────────────

    [Fact]
    public async Task Adapters_Should_NotModifyPayload()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var port = new MockSerialPort("COM1");

        // Act
        var payload = await renderer.RenderAsync(document);
        var beforeBytes = payload.Data.ToArray();

        var adapter = new UsbPrinterAdapter(port, _printerConfig);
        await adapter.PrintAsync(payload);

        // Assert — payload data unchanged after print
        payload.Data.ToArray().Should().BeEquivalentTo(beforeBytes);
    }

    [Fact]
    public async Task LanAdapter_Should_NotModifyPayload()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var client = new MockTcpClient();

        // Act
        var payload = await renderer.RenderAsync(document);
        var beforeBytes = payload.Data.ToArray();

        var adapter = new LanPrinterAdapter(client, _printerConfig);
        await adapter.PrintAsync(payload);

        // Assert
        payload.Data.ToArray().Should().BeEquivalentTo(beforeBytes);
    }

    // ── 4. Renderer Succeeds, Adapter Fails ────────────────────

    [Fact]
    public async Task Pipeline_WhenAdapterFails_Should_ReturnFailedPrintResult()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var port = new MockSerialPort("COM1");
        port.ThrowOnWrite = true;
        var adapter = new UsbPrinterAdapter(port, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        var result = await adapter.PrintAsync(payload);

        // Assert — no exception, failed PrintResult
        result.Success.Should().BeFalse();
        result.Status.Should().Be("Failed");
        result.ErrorMessage.Should().NotBeNull();
    }

    [Fact]
    public async Task Pipeline_WhenLanAdapterFails_Should_ReturnFailedPrintResult()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var client = new MockTcpClient();
        client.ThrowOnConnect = true;
        var adapter = new LanPrinterAdapter(client, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        var result = await adapter.PrintAsync(payload);

        // Assert — no exception, failed PrintResult
        result.Success.Should().BeFalse();
        result.Status.Should().Be("Failed");
    }

    // ── 5. CancellationToken Propagation ──────────────────────

    [Fact]
    public async Task Pipeline_WithCancelledToken_Should_ReturnCancelled()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _printerConfig);

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act
        var payload = await renderer.RenderAsync(document);
        var result = await adapter.PrintAsync(payload, cts.Token);

        // Assert — adapter catches OperationCanceledException and returns cancelled result
        result.Success.Should().BeFalse();
        result.Status.Should().Be("Cancelled");
        result.ErrorMessage.Should().Be("Print was cancelled");
    }

    // ── 6. Timeout ────────────────────────────────────────────

    [Fact]
    public async Task LanAdapter_Timeout_Should_ReturnFailed()
    {
        // Arrange
        var client = new MockTcpClient();
        client.ThrowOnConnect = true;
        var adapter = new LanPrinterAdapter(client, _printerConfig);

        // Act
        var result = await adapter.PrintAsync(new PrintPayload
        {
            Data = Array.Empty<byte>(),
            MimeType = "application/vnd.escpos",
            Format = "escpos"
        });

        // Assert
        result.Success.Should().BeFalse();
    }

    // ── 7. Architectural Isolation ────────────────────────────

    [Fact]
    public void UsbAdapter_Should_NotReference_TcpClient()
    {
        // Arrange
        var usbType = typeof(UsbPrinterAdapter);

        // Act
        var referencedTypes = usbType.GetConstructors()
            .SelectMany(c => c.GetParameters())
            .Select(p => p.ParameterType)
            .ToList();

        // Assert — USB adapter depends on ISerialPort, NOT ITcpClient
        referencedTypes.Should().Contain(typeof(ISerialPort));
        referencedTypes.Should().NotContain(typeof(ITcpClient));
        referencedTypes.Should().NotContain(typeof(System.Net.Sockets.TcpClient));
    }

    [Fact]
    public void LanAdapter_Should_NotReference_SerialPort()
    {
        // Arrange
        var lanType = typeof(LanPrinterAdapter);

        // Act
        var referencedTypes = lanType.GetConstructors()
            .SelectMany(c => c.GetParameters())
            .Select(p => p.ParameterType)
            .ToList();

        // Assert — LAN adapter depends on ITcpClient, NOT ISerialPort
        referencedTypes.Should().Contain(typeof(ITcpClient));
        referencedTypes.Should().NotContain(typeof(ISerialPort));
        referencedTypes.Should().NotContain(typeof(System.IO.Ports.SerialPort));
    }

    // ── 8. Parallel Rendering (Thread Safety) ─────────────────

    [Fact]
    public async Task Renderer_ThreadSafe_ParallelRenders_Should_NotCorrupt()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var documents = Enumerable.Range(0, 10)
            .Select(i => new ReceiptDocument
            {
                Title = $"Doc {i}",
                Lines = new List<ReceiptLine>
                {
                    new() { Text = $"Line {i}-1" },
                    new() { Text = $"Line {i}-2" }
                }
            })
            .ToList();

        // Act — render all documents in parallel
        var tasks = documents.Select(d => renderer.RenderAsync(d));
        var payloads = await Task.WhenAll(tasks);

        // Assert — each payload is unique
        payloads.Select(p => p.Description).Distinct().Should().HaveCount(10);
        payloads.Should().OnlyContain(p => p.Format == "escpos");
        payloads.Should().OnlyContain(p => p.Data.Length > 0);
    }

    // ── 9. Resource Cleanup ────────────────────────────────────

    [Fact]
    public async Task UsbAdapter_Should_ClosePort_AfterPrint()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        await adapter.PrintAsync(payload);

        // Assert — port is closed after print
        port.IsOpen.Should().BeFalse();
        port.CloseCallCount.Should().Be(1);
    }

    [Fact]
    public async Task LanAdapter_Should_Disconnect_AfterPrint()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var client = new MockTcpClient();
        var adapter = new LanPrinterAdapter(client, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        await adapter.PrintAsync(payload);

        // Assert — disconnected after print
        client.IsConnected.Should().BeFalse();
        client.DisconnectCallCount.Should().Be(1);
    }

    [Fact]
    public async Task UsbAdapter_Should_ClosePort_EvenOnFailure()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var port = new MockSerialPort("COM1");
        port.ThrowOnWrite = true;
        var adapter = new UsbPrinterAdapter(port, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        await adapter.PrintAsync(payload);

        // Assert — port closed even after failure
        port.IsOpen.Should().BeFalse();
    }

    [Fact]
    public async Task LanAdapter_Should_Disconnect_EvenOnFailure()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = CreateSampleDocument();
        var client = new MockTcpClient();
        client.IsConnected = true;
        client.ThrowOnSend = true;
        var adapter = new LanPrinterAdapter(client, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        await adapter.PrintAsync(payload);

        // Assert — disconnected after failure
        client.IsConnected.Should().BeFalse();
    }

    // ── 10. Full Document Format Verification ──────────────────

    [Fact]
    public async Task FullDocument_RenderToUsb_Should_ProduceValidEscPos()
    {
        // Arrange
        var renderer = new EscPosRenderer(_renderOptions);
        var document = new ReceiptDocument
        {
            Title = "JLek POS",
            ReprintLabel = "*** REPRINT ***",
            ReceiptNumber = "RC001",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "------------------------------", Alignment = ReceiptAlignment.Center },
                new() { Text = "ผัดไทยกุ้งสด  50.00", Alignment = ReceiptAlignment.Left, Bold = true },
                new() { Text = "  2x = 100.00", Alignment = ReceiptAlignment.Left },
                new() { Text = "รวม  100.00", Alignment = ReceiptAlignment.Right, Bold = true },
                new() { Text = "------------------------------", Alignment = ReceiptAlignment.Center },
                new() { Text = "ขอบคุณครับ", Alignment = ReceiptAlignment.Center }
            }
        };
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _printerConfig);

        // Act
        var payload = await renderer.RenderAsync(document);
        await adapter.PrintAsync(payload);

        // Assert — verify ESC/POS structural elements
        var bytes = port.WrittenData.ToArray();

        // 1. Initialize command at start
        bytes[0].Should().Be(0x1B);
        bytes[1].Should().Be(0x40);

        // 2. Code page command
        bytes[2].Should().Be(0x1B);
        bytes[3].Should().Be(0x74);
        bytes[4].Should().Be(0x11); // Thai CP874

        // 3. Contains text content
        var text = System.Text.Encoding.UTF8.GetString(bytes);
        text.Should().Contain("JLek POS");
        text.Should().Contain("REPRINT");
        text.Should().Contain("RC001");
        text.Should().Contain("ผัดไทยกุ้งสด");

        // 4. Cut command at end
        bytes[^2].Should().Be(0x1D);
        bytes[^1].Should().Be(0x56);

        // 5. Contains alignment and bold commands
        CountEscPosCommands(bytes).Should().BeGreaterThan(5);
    }

    // ── Helper ────────────────────────────────────────────────

    private static int CountEscPosCommands(byte[] data)
    {
        // Count ESC sequences (0x1B followed by command byte)
        int count = 0;
        for (int i = 0; i < data.Length - 1; i++)
        {
            if (data[i] == 0x1B && data[i + 1] != 0x0A) // ESC not LF
                count++;
            if (data[i] == 0x1D) // GS
                count++;
        }
        return count;
    }
}