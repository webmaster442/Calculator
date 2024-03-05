//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;
internal sealed class SetCommand : ShellCommand
{
    public SetCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["set"];

    public override string Synopsys
        => "Sets a variable";

    public override string HelpMessage
        => this.BuildHelpMessage<SetOptions>();

    internal class SetOptions
    {
        [Value(0, HelpText = "Variable name to set", Required = true)]
        public string Name { get; set; }

        [Value(1, HelpText = "Variable value", Required = true)]
        public string Expression { get; set; }

        public SetOptions()
        {
            Name = string.Empty;
            Expression = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<SetOptions>(Host);
        Host.Mediator.Notify(new SetVariable(options.Name, options.Expression));
    }
}
