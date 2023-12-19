using System.Diagnostics;

namespace CalculatorShell.Engine;

public sealed class EngineResult
{
    private readonly Exception? _exception;
    private readonly Number? _number;

    public EngineResult(Exception exception)
    {
        _exception = exception;
    }

    public EngineResult(Number number)
    {
        _number = number;
    }

    public void When(Action<Number> numberHandler, Action<Exception> exceptionHandler)
    {
        if (_number != null)
            numberHandler.Invoke(_number);
        else if (_exception != null)
            exceptionHandler.Invoke(_exception);
        else
            throw new UnreachableException();
    }
}
