using System.Text;

namespace JLek.POS.Printing.Renderer;

public static class EscPosCodePages
{
    private static readonly Dictionary<int, byte> CodePageMap = new()
    {
        { 437, 0x00 },    // CP437 (US/Europe default)
        { 850, 0x02 },    // CP850 (Western Europe)
        { 874, 0x11 },    // CP874 (Thai)
        { 932, 0x01 },    // CP932 (Japanese)
        { 1252, 0x10 },   // Windows-1252
    };

    public static byte GetCodePageId(Encoding encoding)
    {
        if (CodePageMap.TryGetValue(encoding.CodePage, out var id))
            return id;

        return 0x00; // default CP437
    }

    public static Encoding GetEncoding(string encodingName)
    {
        try
        {
            return Encoding.GetEncoding(encodingName);
        }
        catch
        {
            return Encoding.GetEncoding(437); // fallback to CP437
        }
    }

    public static byte[] GetCodePageCommand(Encoding encoding)
    {
        return EscPosCommands.SetCodePage(GetCodePageId(encoding));
    }
}