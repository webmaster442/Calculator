namespace CalculatorShell.Core;

public class CommandException : Exception
{
    public CommandException() : base()
    {
    }

    public CommandException(string? message) : base(message)
    {
    }

    public CommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
