namespace JLek.POS.Printing.Renderer;

public static class EscPosCommands
{
    // Initialize printer
    public static ReadOnlySpan<byte> Initialize => [0x1B, 0x40]; // ESC @

    // Line Feed
    public static ReadOnlySpan<byte> LineFeed => [0x0A]; // LF

    // Horizontal tab
    public static ReadOnlySpan<byte> HorizontalTab => [0x09]; // HT

    // Alignment
    public static byte[] SetAlignment(EscPosAlignment alignment)
    {
        return [0x1B, 0x61, (byte)alignment]; // ESC a n
    }

    // Bold
    public static byte[] SetBold(bool enabled)
    {
        return [0x1B, 0x45, enabled ? (byte)1 : (byte)0]; // ESC E n
    }

    // Double strike
    public static byte[] SetDoubleStrike(bool enabled)
    {
        return [0x1B, 0x47, enabled ? (byte)1 : (byte)0]; // ESC G n
    }

    // Character size (width, height)
    public static byte[] SetCharacterSize(bool doubleWidth, bool doubleHeight)
    {
        byte value = 0;
        if (doubleHeight) value |= 0x10;
        if (doubleWidth) value |= 0x20;
        return [0x1D, 0x21, value]; // GS ! n
    }

    // Reset character size to normal
    public static ReadOnlySpan<byte> ResetCharacterSize => [0x1D, 0x21, 0x00]; // GS ! 0

    // Paper cut
    public static byte[] CutPaper(EscPosCutMode mode)
    {
        return [0x1D, 0x56, (byte)mode]; // GS V m
    }

    // Set code page
    public static byte[] SetCodePage(byte codePageId)
    {
        return [0x1B, 0x74, codePageId]; // ESC t n
    }
}

public enum EscPosAlignment : byte
{
    Left = 0,
    Center = 1,
    Right = 2
}

public enum EscPosCutMode : byte
{
    FullCut = 0,
    PartialCut = 1
}