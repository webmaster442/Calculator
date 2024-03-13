//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

using Calculator.ArgumentCompleters;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

internal sealed class OpenCommaand : ShellCommandAsync
{
    public OpenCommaand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["open"];

    public override string Category
        => CommandCategories.FileManagement;

    public override string Synopsys
        => "Opens the specified file";

    public override string HelpMessage
        => this.BuildHelpMessage<OpenOptions>();

    public override IArgumentCompleter? ArgumentCompleter 
        => new FileNameCompleter(Host);

    internal class OpenOptions
    {
        [Value(0, HelpText = "File name to open", Required = true)]
        public string FileName { get; set; }

        [Option('y', "yes", HelpText = "Do not display confirmation, assume yes to confirmation")]
        public bool Confirmed { get; set; }

        public OpenOptions()
        {
            FileName = string.Empty;
        }
    }

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var options = args.Parse<OpenOptions>(Host);

        if (!File.Exists(options.FileName))
            throw new CommandException($"File doesn't exist: {options.FileName}");

        bool confirmed = options.Confirmed;
        if (!confirmed)
            confirmed = await Host.Dialogs.Confirm("Open file?", cancellationToken);

        if (!confirmed)
            return;

        var extension = Path.GetExtension(options.FileName).ToLower();

        using var process = new Process();
        process.StartInfo.FileName = options.FileName;
        process.StartInfo.UseShellExecute = extension is not ".exe" and not ".com";
        process.Start();
    }
}
