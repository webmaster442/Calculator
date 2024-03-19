//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.ArgumentCompleters;
using Calculator.Messages;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

internal class HistoryCommand : ShellCommandAsync
{
    public HistoryCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["history"];

    public override string Synopsys 
        => "Acces command history";


    public override string Category
        => CommandCategories.Program;

    public override string HelpMessage
        => this.BuildHelpMessage<HistoryOptions>();


    public override IArgumentCompleter? ArgumentCompleter
        => new OptionClassCompleter<HistoryOptions>(Host);

    internal class HistoryOptions
    {
        [Option(
            'e',
            "export",
            SetName = "exporting",
            HelpText = "Export executed commands to a file, that can be parsed by exec",
            Required = false)]
        public string ExportFile { get; set; }

        [Option('d', "delete", SetName = "delete", HelpText = "Delete current history", Required = false)]
        public bool Delete { get; set; }

        public HistoryOptions()
        {
            ExportFile = string.Empty;
        }
    }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var options = args.Parse<HistoryOptions>(Host);

        if (options.Delete)
        {
            Host.Mediator.Notify(new DeleteHistory());
            return;
        }

        var data = Host.Mediator.Request<IEnumerable<string>, HistoryRequestMessage>(new HistoryRequestMessage())
            ?? throw new InvalidOperationException("No history recieved");

        if (!string.IsNullOrEmpty(options.ExportFile))
        {
            await File.WriteAllLinesAsync(options.ExportFile, data, cancellationToken);
            return;
        }
       
        if (!data.Any())
        {
            Host.Output.Result("No command has been executed");
            return;
        }

        var selection = await Host.Dialogs.SelectionList("History", data.Select(x => new SelectionListItem
        {
            Description = x,
            Item = x,
            IsChecked = false
        }), cancellationToken);

        Host.Mediator.Notify(new EnqueCommands(new[] { selection.Description }));
    }
}
