using FluentAssertions;
using JLek.POS.Printing.Models;
using Xunit;

namespace JLek.POS.Printing.Infrastructure.Tests;

public sealed class UsbPrinterAdapterTests
{
    private readonly PrinterConfiguration _config = new()
    {
        PaperWidthMm = 80,
        CharactersPerLine = 48,
        Encoding = "windows-874",
        DefaultAdapter = "usb",
        PrintTimeoutSeconds = 30
    };

    private PrintPayload CreateSamplePayload()
    {
        return new PrintPayload
        {
            Data = new byte[] { 0x1B, 0x40, 0x1B, 0x74, 0x11, 0x0A },
            MimeType = "application/vnd.escpos",
            Format = "escpos",
            Description = "Test receipt"
        };
    }

    [Fact]
    public async Task PrintAsync_Should_OpenPort_WriteData_ClosePort()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _config);
        var payload = CreateSamplePayload();

        // Act
        var result = await adapter.PrintAsync(payload);

        // Assert
        result.Success.Should().BeTrue();
        result.Status.Should().Be("Completed");
        port.OpenCallCount.Should().Be(1);
        port.CloseCallCount.Should().Be(1);
        port.WrittenData.ToArray().Should().BeEquivalentTo(payload.Data.ToArray());
        port.IsOpenAfterWrite.Should().BeFalse();
    }

    [Fact]
    public async Task PrintAsync_WhenAlreadyOpen_Should_NotOpenAgain()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        port.IsOpen = true; // simulate already open
        var adapter = new UsbPrinterAdapter(port, _config);
        var payload = CreateSamplePayload();

        // Act
        var result = await adapter.PrintAsync(payload);

        // Assert
        result.Success.Should().BeTrue();
        port.OpenCallCount.Should().Be(0); // should not call Open
        port.WrittenData.ToArray().Should().BeEquivalentTo(payload.Data.ToArray());
    }

    [Fact]
    public async Task PrintAsync_Should_ReturnPrinterName()
    {
        // Arrange
        var port = new MockSerialPort("COM3");
        var adapter = new UsbPrinterAdapter(port, _config);

        // Act
        var result = await adapter.PrintAsync(CreateSamplePayload());

        // Assert
        result.PrinterName.Should().Contain("USB");
        result.PrinterName.Should().Contain("COM3");
    }

    [Fact]
    public async Task PrintAsync_WhenWriteFails_Should_ReturnFailedResult()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        port.ThrowOnWrite = true;
        var adapter = new UsbPrinterAdapter(port, _config);

        // Act
        var result = await adapter.PrintAsync(CreateSamplePayload());

        // Assert
        result.Success.Should().BeFalse();
        result.Status.Should().Be("Failed");
        result.ErrorMessage.Should().NotBeNull();
    }

    [Fact]
    public async Task PrintAsync_WhenPortThrowsOnOpen_Should_ReturnFailedResult()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        port.ThrowOnOpen = true;
        var adapter = new UsbPrinterAdapter(port, _config);

        // Act
        var result = await adapter.PrintAsync(CreateSamplePayload());

        // Assert
        result.Success.Should().BeFalse();
        result.Status.Should().Be("Failed");
        result.ErrorMessage.Should().NotBeNull();
    }

    [Fact]
    public async Task PrintAsync_Should_IncludeTimestamps()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _config);

        // Act
        var result = await adapter.PrintAsync(CreateSamplePayload());

        // Assert
        result.StartedAt.Should().NotBe(default);
        result.FinishedAt.Should().NotBe(default);
        result.FinishedAt.Should().BeOnOrAfter(result.StartedAt);
    }

    [Fact]
    public async Task PrintAsync_PayloadUnchanged_Should_NotModifyData()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _config);
        var original = new byte[] { 0x1B, 0x40, 0x0A };
        var payload = new PrintPayload
        {
            Data = original,
            MimeType = "application/vnd.escpos",
            Format = "escpos"
        };

        // Act
        await adapter.PrintAsync(payload);

        // Assert
        port.WrittenData.ToArray().Should().BeEquivalentTo(original);
    }

    [Fact]
    public async Task PrintAsync_AfterFailure_Should_StillClosePort()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        port.ThrowOnWrite = true;
        var adapter = new UsbPrinterAdapter(port, _config);

        // Act
        await adapter.PrintAsync(CreateSamplePayload());

        // Assert
        port.CloseCallCount.Should().Be(1);
        port.IsOpenAfterWrite.Should().BeFalse();
    }

    [Fact]
    public async Task GetStatusAsync_WhenPortOpens_Should_ReturnOnline()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        var adapter = new UsbPrinterAdapter(port, _config);

        // Act
        var status = await adapter.GetStatusAsync();

        // Assert
        status.IsOnline.Should().BeTrue();
    }

    [Fact]
    public async Task GetStatusAsync_WhenPortFails_Should_ReturnOffline()
    {
        // Arrange
        var port = new MockSerialPort("COM1");
        port.ThrowOnOpen = true;
        var adapter = new UsbPrinterAdapter(port, _config);

        // Act
        var status = await adapter.GetStatusAsync();

        // Assert
        status.IsOnline.Should().BeFalse();
    }

    // ── LanPrinterAdapter Tests ──────────────────────────────────

    [Fact]
    public async Task LanPrintAsync_Should_Connect_SendData_Disconnect()
    {
        // Arrange
        var client = new MockTcpClient();
        var adapter = new LanPrinterAdapter(client, _config);
        var payload = new PrintPayload
        {
            Data = new byte[] { 0x1B, 0x40, 0x0A },
            MimeType = "application/vnd.escpos",
            Format = "escpos"
        };

        // Act
        var result = await adapter.PrintAsync(payload);

        // Assert
        result.Success.Should().BeTrue();
        result.Status.Should().Be("Completed");
        client.ConnectCallCount.Should().Be(1);
        client.SentData.ToArray().Should().BeEquivalentTo(payload.Data.ToArray());
        client.DisconnectCallCount.Should().Be(1);
        result.PrinterName.Should().Contain("LAN");
    }

    [Fact]
    public async Task LanPrintAsync_WhenConnectionFails_Should_ReturnFailed()
    {
        // Arrange
        var client = new MockTcpClient();
        client.ThrowOnConnect = true;
        var adapter = new LanPrinterAdapter(client, _config);

        // Act
        var result = await adapter.PrintAsync(new PrintPayload
        {
            Data = Array.Empty<byte>(),
            MimeType = "application/vnd.escpos",
            Format = "escpos"
        });

        // Assert
        result.Success.Should().BeFalse();
        result.Status.Should().Be("Failed");
    }

    [Fact]
    public async Task LanPrintAsync_WhenSendFails_Should_ReturnFailed()
    {
        // Arrange
        var client = new MockTcpClient();
        client.ThrowOnSend = true;
        var adapter = new LanPrinterAdapter(client, _config);

        // Act
        var result = await adapter.PrintAsync(new PrintPayload
        {
            Data = new byte[] { 0x1B },
            MimeType = "application/vnd.escpos",
            Format = "escpos"
        });

        // Assert
        result.Success.Should().BeFalse();
        result.Status.Should().Be("Failed");
    }

    [Fact]
    public async Task LanPrintAsync_AfterFailure_Should_StillDisconnect()
    {
        // Arrange
        var client = new MockTcpClient();
        client.IsConnected = true;
        client.ThrowOnSend = true;
        var adapter = new LanPrinterAdapter(client, _config);

        // Act
        await adapter.PrintAsync(new PrintPayload
        {
            Data = new byte[] { 0x1B },
            MimeType = "application/vnd.escpos",
            Format = "escpos"
        });

        // Assert
        client.DisconnectCallCount.Should().Be(1);
    }

    [Fact]
    public async Task LanGetStatusAsync_WhenConnected_Should_ReturnOnline()
    {
        // Arrange
        var client = new MockTcpClient();
        var adapter = new LanPrinterAdapter(client, _config);

        // Act
        var status = await adapter.GetStatusAsync();

        // Assert
        status.IsOnline.Should().BeTrue();
    }

    [Fact]
    public async Task LanGetStatusAsync_WhenConnectionFails_Should_ReturnOffline()
    {
        // Arrange
        var client = new MockTcpClient();
        client.ThrowOnConnect = true;
        var adapter = new LanPrinterAdapter(client, _config);

        // Act
        var status = await adapter.GetStatusAsync();

        // Assert
        status.IsOnline.Should().BeFalse();
    }

    [Fact]
    public async Task LanPrintAsync_Should_IncludeTimestamps()
    {
        // Arrange
        var client = new MockTcpClient();
        var adapter = new LanPrinterAdapter(client, _config);

        // Act
        var result = await adapter.PrintAsync(new PrintPayload
        {
            Data = Array.Empty<byte>(),
            MimeType = "application/vnd.escpos",
            Format = "escpos"
        });

        // Assert
        result.StartedAt.Should().NotBe(default);
        result.FinishedAt.Should().NotBe(default);
        result.FinishedAt.Should().BeOnOrAfter(result.StartedAt);
    }
}

public sealed class MockSerialPort : ISerialPort
{
    private readonly List<byte> _written = new();

    public MockSerialPort(string portName)
    {
        PortName = portName;
    }

    public string PortName { get; }
    public bool IsOpen { get; set; }
    public int OpenCallCount { get; private set; }
    public int CloseCallCount { get; private set; }
    public IReadOnlyList<byte> WrittenData => _written;
    public bool IsOpenAfterWrite { get; private set; } = true;
    public bool ThrowOnOpen { get; set; }
    public bool ThrowOnWrite { get; set; }

    public void Open()
    {
        if (ThrowOnOpen)
            throw new IOException("Port cannot be opened");

        OpenCallCount++;
        IsOpen = true;
    }

    public void Close()
    {
        CloseCallCount++;
        IsOpen = false;
    }

    public void Write(ReadOnlySpan<byte> data)
    {
        if (ThrowOnWrite)
            throw new IOException("Write failed");

        _written.AddRange(data.ToArray());
    }

    public void Dispose()
    {
        IsOpen = false;
    }
}

public sealed class MockTcpClient : ITcpClient
{
    private readonly List<byte> _sent = new();

    public bool IsConnected { get; set; }
    public int ConnectCallCount { get; private set; }
    public int DisconnectCallCount { get; private set; }
    public IReadOnlyList<byte> SentData => _sent;
    public bool ThrowOnConnect { get; set; }
    public bool ThrowOnSend { get; set; }

    public Task ConnectAsync(string host, int port, CancellationToken cancellationToken = default)
    {
        if (ThrowOnConnect)
            throw new System.Net.Sockets.SocketException(10061); // Connection refused

        ConnectCallCount++;
        IsConnected = true;
        return Task.CompletedTask;
    }

    public Task SendAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    {
        if (ThrowOnSend)
            throw new IOException("Send failed");

        _sent.AddRange(data.ToArray());
        return Task.CompletedTask;
    }

    public Task DisconnectAsync()
    {
        DisconnectCallCount++;
        IsConnected = false;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        IsConnected = false;
    }
}
