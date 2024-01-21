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
        var response = Host.Mediator.Request<IEnumerable<KeyValuePair<string, Number>>, VariableListMessage>(new VariableListMessage());
        if (response != null && response.Any())
        {
            TableData table = new TableData("Variable", "Value");
            foreach (var variable in response)
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
