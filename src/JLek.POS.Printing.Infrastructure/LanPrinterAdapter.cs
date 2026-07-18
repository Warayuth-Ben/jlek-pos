using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Printing.Abstractions;
using JLek.POS.Printing.Models;

namespace JLek.POS.Printing.Infrastructure;

public sealed class LanPrinterAdapter : IPrinterAdapter
{
    private readonly ITcpClient _tcpClient;
    private readonly PrinterConfiguration _config;

    public LanPrinterAdapter(ITcpClient tcpClient, PrinterConfiguration config)
    {
        _tcpClient = tcpClient;
        _config = config;
    }

    public async Task<PrintResult> PrintAsync(
        PrintPayload payload,
        CancellationToken cancellationToken = default)
    {
        var startedAt = DateTime.UtcNow;

        try
        {
            var host = _config.IpAddress ?? "192.168.1.100";
            var port = _config.Port > 0 ? _config.Port : 9100;

            // Connect
            await _tcpClient.ConnectAsync(host, port, cancellationToken);

            // Send payload bytes
            await _tcpClient.SendAsync(payload.Data, cancellationToken);

            return new PrintResult
            {
                Success = true,
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = "Completed",
                PrinterName = $"LAN ({host}:{port})",
                Copies = 1
            };
        }
        catch (OperationCanceledException)
        {
            return new PrintResult
            {
                Success = false,
                ErrorMessage = "Print was cancelled",
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = "Cancelled",
                PrinterName = "LAN",
                Copies = 1
            };
        }
        catch (TimeoutException ex)
        {
            return new PrintResult
            {
                Success = false,
                ErrorMessage = $"Timeout: {ex.Message}",
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = "Failed",
                PrinterName = "LAN",
                Copies = 1
            };
        }
        catch (System.Net.Sockets.SocketException ex)
        {
            return new PrintResult
            {
                Success = false,
                ErrorMessage = $"Socket error: {ex.Message}",
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = "Failed",
                PrinterName = "LAN",
                Copies = 1
            };
        }
        catch (IOException ex)
        {
            return new PrintResult
            {
                Success = false,
                ErrorMessage = $"IO Error: {ex.Message}",
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = "Failed",
                PrinterName = "LAN",
                Copies = 1
            };
        }
        catch (Exception ex)
        {
            return new PrintResult
            {
                Success = false,
                ErrorMessage = $"Unexpected error: {ex.Message}",
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = "Failed",
                PrinterName = "LAN",
                Copies = 1
            };
        }
        finally
        {
            if (_tcpClient.IsConnected)
            {
                await _tcpClient.DisconnectAsync();
            }
        }
    }

    public async Task<PrinterStatus> GetStatusAsync()
    {
        try
        {
            var host = _config.IpAddress ?? "192.168.1.100";
            var port = _config.Port > 0 ? _config.Port : 9100;

            await _tcpClient.ConnectAsync(host, port);
            await _tcpClient.DisconnectAsync();

            return new PrinterStatus
            {
                IsOnline = true,
                HasPaper = true, // Unknown in v1
                IsBusy = false,
                ErrorMessage = null
            };
        }
        catch
        {
            return new PrinterStatus
            {
                IsOnline = false,
                HasPaper = false,
                IsBusy = false,
                ErrorMessage = "Cannot connect to printer"
            };
        }
    }
}