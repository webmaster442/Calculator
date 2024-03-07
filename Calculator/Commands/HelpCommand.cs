using Calculator.Messages;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;
internal class HelpCommand : ShellCommand
{
    public HelpCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["help"];

    public override string Synopsys
        => "Provides help information for a given command";

    public override string Category
        => CommandCategories.Program;

    public override string HelpMessage
        => this.BuildHelpMessage<HelpOptions>();

    internal class HelpOptions
    {
        [Value(0, HelpText = "Command name", Required = true)]
        public string CommandName { get; set; }

        public HelpOptions()
        {
            CommandName = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<HelpOptions>(Host);

        var help = Host.Mediator.Request<string, HelpRequestMessage>(new HelpRequestMessage(options.CommandName));

        if (help != null)
            Host.Output.Result(help);
    }
}
