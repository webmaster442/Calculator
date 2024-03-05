//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.ArgumentCompleters;
using Calculator.Messages;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

internal sealed class CdCommand : ShellCommandAsync
{
    public CdCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["cd"];

    public override string Synopsys
        => "Changes the current working directory";

    public override IArgumentCompleter? ArgumentCompleter
        => new DirectoryNameCompleter(Host);

    public override string HelpMessage
        => this.BuildHelpMessage<CdOptions>();

    internal class CdOptions
    {
        [Value(0, HelpText = "Directory path")]
        public string Path { get; set; }

        public CdOptions()
        {
            Path = string.Empty;
        }
    }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var options = args.Parse<CdOptions>(Host);

        if (string.IsNullOrEmpty(options.Path))
        {
            options.Path = await Host.Dialogs.SelectDirectory(cancellationToken);
        }
        Host.Mediator.Notify(new SetCurrentDir(options.Path));
    }
}
