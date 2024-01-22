using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

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

    private static Options CreateFromSelection(IEnumerable<SelectionListItem> wassettrue)
    {
        Options ret = new();
        HashSet<string> trueProperties = wassettrue.Select(x => x.Item).ToHashSet();

        var properties = typeof(Options)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite);

        foreach (var property in properties)
        {
            if (trueProperties.Contains(property.Name))
                property.SetValue(ret, true);
            else
                property.SetValue(ret, false);
        }

        return ret;
    }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var options = Host.Mediator.Request<Options, OptionsRequest>(new OptionsRequest())
            ?? throw new CommandException("Can't get options");

        var currentOptions = GetOptions(options);

        var selected = await Host.Dialogs.SelectionList("Program options", currentOptions, cancellationToken);

        var modified = CreateFromSelection(selected);

        Host.Mediator.Notify(new SetOptions(modified));

        var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "calculator.json");
        using var stream = File.OpenRead(fileName);

        await JsonSerializer.SerializeAsync(stream, modified);
    }
}
