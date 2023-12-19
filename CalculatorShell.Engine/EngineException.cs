namespace CalculatorShell.Engine;

public class EngineException : Exception
{
    public static EngineException CreateTypeMismatch<TTarget>(NumberType numberType)
    {
        return new EngineException($"Can't cast {numberType} to {typeof(TTarget)}");
    }

    internal static EngineException CreateArithmetic(Number left, Number right, char op)
    {
        return new EngineException($"Can't perform operation of '{op}' on {left.NumberType} and {right.NumberType}");
    }

    internal static Exception CreateNumberParse(string? number)
    {
        return new EngineException($"{number} can't be parsed");
    }

    internal static Exception DataLoss<T>()
    {
        throw new EngineException($"{typeof(T)} can't represent original value");
    }

    public EngineException()
    {
    }

    public EngineException(string? message) : base(message)
    {
    }

    public EngineException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
