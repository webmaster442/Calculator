//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

internal sealed class ExecCommand : ShellCommand
{
    public ExecCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["exec"];


    public override string Category
        => CommandCategories.Program;

    public override string Synopsys
        => "Executes a file containing calculator commands";

    public override string HelpMessage
        => this.BuildHelpMessage<ExecOptions>();

    internal sealed class ExecOptions
    {
        [Value(0, HelpText = "File name to execute", Required = true)]
        public string FileName { get; set; }

        public ExecOptions()
        {
            FileName = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<ExecOptions>(Host);

        FileInfo fi = new(options.FileName);

        if (!fi.Exists)
            throw new CommandException($"File doesn't exist: {args[0]}");

        if (fi.Length > 32 * 1024)
            throw new CommandException("File contains to many instructions > 32k");

        var instructions = File.ReadAllLines(args[0])
            .Where(x => !string.IsNullOrWhiteSpace(x) && !x.StartsWith('#'))
            .ToArray();

        Host.Mediator.Notify(new EnqueCommands(instructions));
    }
}
