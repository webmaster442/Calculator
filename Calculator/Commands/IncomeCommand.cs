using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal class IncomeCommand : ExpenseCommand
{
    public IncomeCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["income"];

    public override string Synopsys
        => "Add an income to the expenses";

    public override void ExecuteInternal(Arguments args)
    {
        var item = Create(args, true);
        Host.Mediator.Notify(new AddExpenseMessage(item));
    }
}