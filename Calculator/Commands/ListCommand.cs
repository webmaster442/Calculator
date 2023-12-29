using Calculator.Messages;

using CalculatorShell.Core;
using CalculatorShell.Engine;

namespace Calculator.Commands;

internal sealed class ListCommand : ShellCommand
{
    public ListCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["list"];

    public override string Synopsys
        => "Lists currently set variables";

    public override void ExecuteInternal(Arguments args)
    {
        var responses = Host.MessageBus.Request<IEnumerable<KeyValuePair<string, Number>>, VariableListMessage>(new VariableListMessage(Guid.Empty));
        var data = responses.FirstOrDefault();
        if (data != null && data.Any())
        {
            TableData table = new TableData("Variable", "Value");
            foreach (var variable in data)
            {
                table.AddRow(variable.Key, variable.Value.ToString(Host.CultureInfo));
            }
            Host.Output.Table(table);
        }
        else
        {
            Host.Output.Result("No variables are currently set");
        }
    }
}
