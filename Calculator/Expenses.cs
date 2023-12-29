using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Expenses;

namespace Calculator;

internal sealed class Expenses : IMessageClient<AddExpenseMessage>
{
    private readonly ExpenseItemDb _db;
    private readonly string _folder;

    public Expenses(IHost host)
    {
        _folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CalculatorShell");
        _db = new ExpenseItemDb(_folder);
        ClientId = new Guid("DA687986-5145-49D6-8AC2-693809C863F7");
        host.MessageBus.RegisterComponent(this);
    }

    public Guid ClientId { get; }

    void IMessageClient<AddExpenseMessage>.ProcessMessage(AddExpenseMessage input) 
        => _db.Insert(input.Item);
}
