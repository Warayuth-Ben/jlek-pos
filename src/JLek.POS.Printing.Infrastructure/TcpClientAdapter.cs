using System.Net.Sockets;

namespace JLek.POS.Printing.Infrastructure;

public sealed class TcpClientAdapter : ITcpClient
{
    private TcpClient? _client;
    private NetworkStream? _stream;

    public bool IsConnected => _client?.Connected ?? false;

    public async Task ConnectAsync(string host, int port, CancellationToken cancellationToken = default)
    {
        _client?.Dispose();
        _client = new TcpClient();

        using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

        await _client.ConnectAsync(host, port, linkedCts.Token);
        _stream = _client.GetStream();
    }

    public async Task SendAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    {
        if (_stream is null)
            throw new InvalidOperationException("Not connected. Call ConnectAsync first.");

        await _stream.WriteAsync(data, cancellationToken);
        await _stream.FlushAsync(cancellationToken);
    }

    public Task DisconnectAsync()
    {
        try
        {
            _stream?.Close();
            _stream?.Dispose();
            _stream = null;

            _client?.Close();
            _client?.Dispose();
            _client = null;

            return Task.CompletedTask;
        }
        catch
        {
            return Task.CompletedTask;
        }
    }

    public void Dispose()
    {
        _stream?.Close();
        _stream?.Dispose();
        _client?.Close();
        _client?.Dispose();
    }
}