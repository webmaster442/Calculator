using Calculator.Internal;

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal class SetOptions : PayloadBase
{
    public SetOptions(Options options)
    {
        Options = options;
    }

    public Options Options { get; }
}