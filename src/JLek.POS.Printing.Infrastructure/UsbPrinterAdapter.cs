using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Printing.Abstractions;
using JLek.POS.Printing.Models;

namespace JLek.POS.Printing.Infrastructure;

public sealed class UsbPrinterAdapter : IPrinterAdapter
{
    private readonly ISerialPort _serialPort;
    private readonly PrinterConfiguration _config;

    public UsbPrinterAdapter(ISerialPort serialPort, PrinterConfiguration config)
    {
        _serialPort = serialPort;
        _config = config;
    }

    public async Task<PrintResult> PrintAsync(
        PrintPayload payload,
        CancellationToken cancellationToken = default)
    {
        var startedAt = DateTime.UtcNow;

        try
        {
            // Open if not already open
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }

            // Write the payload data
            _serialPort.Write(payload.Data.Span);

            // Small delay to ensure printer buffers are flushed
            await Task.Delay(100, cancellationToken);

            return new PrintResult
            {
                Success = true,
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = "Completed",
                PrinterName = $"USB ({_serialPort.PortName})",
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
                PrinterName = $"USB ({_serialPort.PortName})",
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
                PrinterName = $"USB ({_serialPort.PortName})",
                Copies = 1
            };
        }
        catch (UnauthorizedAccessException ex)
        {
            return new PrintResult
            {
                Success = false,
                ErrorMessage = $"Access denied: {ex.Message}",
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = "Failed",
                PrinterName = $"USB ({_serialPort.PortName})",
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
                PrinterName = $"USB ({_serialPort.PortName})",
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
                PrinterName = $"USB ({_serialPort.PortName})",
                Copies = 1
            };
        }
        finally
        {
            // Close the port after each print job
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }
    }

    public Task<PrinterStatus> GetStatusAsync()
    {
        try
        {
            // v1: Check if port can be opened
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
                _serialPort.Close();
            }

            return Task.FromResult(new PrinterStatus
            {
                IsOnline = true,
                HasPaper = true, // Unknown in v1
                IsBusy = false,
                ErrorMessage = null
            });
        }
        catch
        {
            return Task.FromResult(new PrinterStatus
            {
                IsOnline = false,
                HasPaper = false,
                IsBusy = false,
                ErrorMessage = "Cannot open port"
            });
        }
    }
}