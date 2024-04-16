using System.Text;

namespace CalculatorShell.Engine.MathComponents;

public sealed class StringBuilderBrainfuckIo : IBrainFuckIO
{
    private string _inputBuffer;
    private int _bufferIndex;
    private readonly StringBuilder _outBuffer;

    public StringBuilderBrainfuckIo()
    {
        _inputBuffer = string.Empty;
        _bufferIndex = 0;
        _outBuffer = new StringBuilder();
    }

    public void SetInputBuffer(string text)
    {
        _inputBuffer = text;
        _bufferIndex = 0;
    }

    public void Print(byte c)
        => _outBuffer.Append((char)c);

    public byte Read()
    {
        if (_bufferIndex < _inputBuffer.Length)
        {
            return (byte)_inputBuffer[_bufferIndex];
        }
        throw new InvalidOperationException("Out of input");
    }

    public override string ToString()
        => _outBuffer.ToString();
}