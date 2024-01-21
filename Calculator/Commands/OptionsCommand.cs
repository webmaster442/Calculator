using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Calculator.Internal;
using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;
internal class OptionsCommand : ShellCommandAsync
{
    public OptionsCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["options"];

    public override string Synopsys
        => "Get or set calculator options";

    private static SelectionListItem[] GetOptions(Options instance)
    {
        return typeof(Options)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite)
            .Select(p => new SelectionListItem
            {
                Item = p.Name,
                Description = p.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "Unknown",
                IsChecked = (bool)p.GetValue(instance)!
            })
            .ToArray();
    }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var options = Host.Mediator.Request<Options, OptionsRequest>(new OptionsRequest())
            ?? throw new CommandException("Can't get options");

        var currentOptions = GetOptions(options);

        var selected = await Host.Dialogs.SelectionList("Program options", currentOptions, cancellationToken);
    }
}
