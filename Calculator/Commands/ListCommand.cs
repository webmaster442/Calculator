//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        var response = Host.Mediator.Request<IEnumerable<KeyValuePair<string, Number>>, VariableListRequest>(new VariableListRequest());
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
