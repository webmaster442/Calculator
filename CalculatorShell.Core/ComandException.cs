namespace CalculatorShell.Core;

public class ComandException : Exception
{
    public ComandException() : base()
    {
    }

    public ComandException(string? message) : base(message)
    {
    }

    public ComandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
