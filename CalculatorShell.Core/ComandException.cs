//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
