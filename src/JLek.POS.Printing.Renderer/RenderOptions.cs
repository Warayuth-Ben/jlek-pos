using System.Text;

namespace JLek.POS.Printing.Renderer;

public sealed record RenderOptions
{
    public int CharactersPerLine { get; init; } = EscPosConstants.DefaultCharactersPerLine;
    public string EncodingName { get; init; } = EscPosConstants.DefaultEncoding;
    public bool CutPaper { get; init; } = true;
    public Encoding Encoding => EscPosCodePages.GetEncoding(EncodingName);
}