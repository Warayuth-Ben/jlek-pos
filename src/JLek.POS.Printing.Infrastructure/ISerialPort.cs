namespace JLek.POS.Printing.Infrastructure;

public interface ISerialPort : IDisposable
{
    string PortName { get; }
    bool IsOpen { get; }
    void Open();
    void Close();
    void Write(ReadOnlySpan<byte> data);
}