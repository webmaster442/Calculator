namespace CalculatorShell.Core.Mediator;

public abstract class PayloadBase
{
    public DateTime DispatchTime { get; }

    protected PayloadBase()
    {
        DispatchTime = DateTime.Now;
    }
}
