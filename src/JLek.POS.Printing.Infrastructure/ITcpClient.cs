namespace JLek.POS.Printing.Infrastructure;

public interface ITcpClient : IDisposable
{
    bool IsConnected { get; }
    Task ConnectAsync(string host, int port, CancellationToken cancellationToken = default);
    Task SendAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default);
    Task DisconnectAsync();
}