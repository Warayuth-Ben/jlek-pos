namespace JLek.POS.Printing.Models;

public sealed record PrinterConfiguration
{
    public int PaperWidthMm { get; init; } = 80;
    public int CharactersPerLine { get; init; } = 48;
    public string Encoding { get; init; } = "windows-874";
    public string DefaultAdapter { get; init; } = "null";
    public int PrintTimeoutSeconds { get; init; } = 30;
    public string? IpAddress { get; init; }
    public int Port { get; init; } = 9100;
    public string? PortName { get; init; }
    public int BaudRate { get; init; } = 9600;
}
