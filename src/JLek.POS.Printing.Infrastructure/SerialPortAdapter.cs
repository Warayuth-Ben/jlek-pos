using System.IO.Ports;

namespace JLek.POS.Printing.Infrastructure;

public sealed class SerialPortAdapter : ISerialPort
{
    private readonly SerialPort _port;

    public SerialPortAdapter(string portName, int baudRate = 9600)
    {
        _port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
        {
            ReadTimeout = 1000,
            WriteTimeout = 3000
        };
    }

    public string PortName => _port.PortName;
    public bool IsOpen => _port.IsOpen;

    public void Open()
    {
        if (!_port.IsOpen)
            _port.Open();
    }

    public void Close()
    {
        if (_port.IsOpen)
            _port.Close();
    }

    public void Write(ReadOnlySpan<byte> data)
    {
        _port.Write(data.ToArray(), 0, data.Length);
    }

    public void Dispose()
    {
        if (_port.IsOpen)
            _port.Close();
        _port.Dispose();
    }
}