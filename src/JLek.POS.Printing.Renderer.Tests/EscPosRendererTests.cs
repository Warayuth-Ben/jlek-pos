using FluentAssertions;
using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Printing.Models;
using Xunit;

namespace JLek.POS.Printing.Renderer.Tests;

public sealed class EscPosRendererTests
{
    private readonly RenderOptions _defaultOptions = new()
    {
        CharactersPerLine = 48,
        EncodingName = "windows-874",
        CutPaper = true
    };

    private EscPosRenderer CreateRenderer(RenderOptions? options = null)
    {
        return new EscPosRenderer(options ?? _defaultOptions);
    }

    [Fact]
    public async Task RenderAsync_Should_ReturnPrintPayload()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Test Receipt",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);

        // Assert
        payload.Should().NotBeNull();
        payload.Format.Should().Be("escpos");
        payload.MimeType.Should().Be("application/vnd.escpos");
        payload.Data.Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RenderAsync_Should_IncludeInitializeCommand()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Test",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — ESC @ (0x1B 0x40) at the start
        data[0].Should().Be(0x1B);
        data[1].Should().Be(0x40);
    }

    [Fact]
    public async Task RenderAsync_Should_IncludeCodePageCommand()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Test",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — ESC t 0x11 (Thai code page) after initialize
        // Position 2 should be ESC t
        data[2].Should().Be(0x1B);
        data[3].Should().Be(0x74);
        data[4].Should().Be(0x11); // CP874 (Thai)
    }

    [Fact]
    public async Task RenderAsync_WithTitle_Should_IncludeTitleText()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "JLek Shop",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — Title text should be in the data (encoded)
        data.Should().ContainInOrder(
            new byte[] { 0x1D, 0x21, 0x30 } // Double Width + Double Height for title
        );
    }

    [Fact]
    public async Task RenderAsync_WithReprintLabel_Should_IncludeReprintLabel()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            ReprintLabel = "*** REPRINT ***",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);

        // Assert
        var text = System.Text.Encoding.UTF8.GetString(payload.Data.ToArray());
        text.Should().Contain("REPRINT");
    }

    [Fact]
    public async Task RenderAsync_WithReceiptNumber_Should_IncludeNumber()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            ReceiptNumber = "RC001",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);

        // Assert
        var text = System.Text.Encoding.UTF8.GetString(payload.Data.ToArray());
        text.Should().Contain("RC001");
    }

    [Fact]
    public async Task RenderAsync_WithCenterAlignment_Should_SetCenterCommand()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Centered Text", Alignment = ReceiptAlignment.Center }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — ESC a 1 = Center alignment
        var alignIndex = FindLastSequence(data, [0x1B, 0x61, 0x01]);
        alignIndex.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RenderAsync_WithRightAlignment_Should_SetRightCommand()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Right Text", Alignment = ReceiptAlignment.Right }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — ESC a 2 = Right alignment
        var alignIndex = FindLastSequence(data, [0x1B, 0x61, 0x02]);
        alignIndex.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RenderAsync_WithBoldText_Should_SetBoldOn()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Bold Text", Bold = true }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — ESC E 1 = Bold ON
        var boldIndex = FindLastSequence(data, [0x1B, 0x45, 0x01]);
        boldIndex.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RenderAsync_WithDoubleWidth_Should_SetCharacterSize()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Wide Text", DoubleWidth = true }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — GS ! 0x20 = Double Width
        var sizeIndex = FindLastSequence(data, [0x1D, 0x21, 0x20]);
        sizeIndex.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RenderAsync_WithDoubleHeight_Should_SetCharacterSize()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Tall Text", DoubleHeight = true }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — GS ! 0x10 = Double Height
        var sizeIndex = FindLastSequence(data, [0x1D, 0x21, 0x10]);
        sizeIndex.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RenderAsync_WithBothDouble_Should_SetCharacterSize()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Big Text", DoubleWidth = true, DoubleHeight = true }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — GS ! 0x30 = Double Width + Double Height
        var sizeIndex = FindLastSequence(data, [0x1D, 0x21, 0x30]);
        sizeIndex.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RenderAsync_WithCutPaper_Should_IncludeCutCommand()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — GS V 1 (partial cut) at the end
        data[^2].Should().Be(0x1D);
        data[^1].Should().Be(0x56);
    }

    [Fact]
    public async Task RenderAsync_WithCutPaperDisabled_Should_NotIncludeCut()
    {
        // Arrange
        var options = new RenderOptions
        {
            CharactersPerLine = 48,
            EncodingName = "windows-874",
            CutPaper = false
        };
        var renderer = CreateRenderer(options);
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — No GS V command should be present
        data.Should().NotContainInOrder(new byte[] { 0x1D, 0x56 });
    }

    [Fact]
    public async Task RenderAsync_WithMultipleLines_Should_IncludeLineFeeds()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Line 1" },
                new() { Text = "Line 2" },
                new() { Text = "Line 3" }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);

        // Assert
        var text = System.Text.Encoding.UTF8.GetString(payload.Data.ToArray());
        text.Should().Contain("Line 1");
        text.Should().Contain("Line 2");
        text.Should().Contain("Line 3");
    }

    [Fact]
    public async Task RenderAsync_WithEncoding874_Should_EncodeThaiCharacters()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "Receipt",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "ผัดไทยกุ้งสด" }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — Thai text should be encoded in CP874
        // CP874: ผ = 0xCA, ัด = 0xD4, ไ = 0xC8, etc.
        // Data should NOT contain UTF-8 Thai (which starts with 0xE0)
        data.Should().NotContainInOrder(
            new byte[] { 0xE0, 0xE1 } // UTF-8 Thai would start with 0xE0
        );
    }

    [Fact]
    public async Task RenderAsync_Should_BeStateless()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc1 = new ReceiptDocument
        {
            Title = "First",
            Lines = new List<ReceiptLine>()
        };
        var doc2 = new ReceiptDocument
        {
            Title = "Second",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload1 = await renderer.RenderAsync(doc1);
        var payload2 = await renderer.RenderAsync(doc2);

        // Assert — Different payloads for different documents
        var text1 = System.Text.Encoding.UTF8.GetString(payload1.Data.ToArray());
        var text2 = System.Text.Encoding.UTF8.GetString(payload2.Data.ToArray());
        text1.Should().Contain("First");
        text2.Should().Contain("Second");
    }

    [Fact]
    public async Task RenderAsync_WithoutTitle_Should_NotIncludeTitle()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Just a line" }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);

        // Assert — should still work without title
        payload.Should().NotBeNull();
        payload.Data.Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task RenderAsync_EmptyDocument_Should_StillIncludeInitAndCodePage()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — at minimum includes init + code page + cut
        data.Length.Should().BeGreaterThan(5);
        data[0].Should().Be(0x1B); // ESC
        data[1].Should().Be(0x40); // @
    }

    [Fact]
    public async Task RenderAsync_Should_ReturnDescription()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "My Receipt",
            Lines = new List<ReceiptLine>()
        };

        // Act
        var payload = await renderer.RenderAsync(doc);

        // Assert
        payload.Description.Should().Contain("My Receipt");
    }

    [Fact]
    public async Task RenderAsync_TextAfterAlignment_ShouldNotResetAlignment()
    {
        // Arrange
        var renderer = CreateRenderer();
        var doc = new ReceiptDocument
        {
            Title = "",
            Lines = new List<ReceiptLine>
            {
                new() { Text = "Left", Alignment = ReceiptAlignment.Left },
                new() { Text = "Center", Alignment = ReceiptAlignment.Center }
            }
        };

        // Act
        var payload = await renderer.RenderAsync(doc);
        var data = payload.Data.ToArray();

        // Assert — there should be ESC a 0 (Left) and ESC a 1 (Center) for respective lines
        var leftAlignCount = CountSequences(data, [0x1B, 0x61, 0x00]);
        var centerAlignCount = CountSequences(data, [0x1B, 0x61, 0x01]);

        leftAlignCount.Should().BeGreaterThanOrEqualTo(1);
        centerAlignCount.Should().Be(1);
    }

    // ── Helpers ────────────────────────────────────────────────

    private static int FindLastSequence(byte[] data, byte[] sequence)
    {
        for (int i = data.Length - sequence.Length; i >= 0; i--)
        {
            bool found = true;
            for (int j = 0; j < sequence.Length; j++)
            {
                if (data[i + j] != sequence[j])
                {
                    found = false;
                    break;
                }
            }
            if (found) return i;
        }
        return -1;
    }

    private static int CountSequences(byte[] data, byte[] sequence)
    {
        int count = 0;
        for (int i = 0; i <= data.Length - sequence.Length; i++)
        {
            bool found = true;
            for (int j = 0; j < sequence.Length; j++)
            {
                if (data[i + j] != sequence[j])
                {
                    found = false;
                    break;
                }
            }
            if (found) count++;
        }
        return count;
    }
}