//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Represents an exception that occured during command run
/// </summary>
public class CommandException : Exception
{
    /// <inheritdoc/>
    public CommandException() : base()
    {
    }

    /// <inheritdoc/>
    public CommandException(string? message) : base(message)
    {
    }

    /// <inheritdoc/>
    public CommandException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
