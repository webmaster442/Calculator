//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.ArgumentCompleters;
using Calculator.Messages;

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

internal sealed class UnSetCommand : ShellCommand
{
    public UnSetCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["unset"];

    public override string Synopsys
        => "Unset a variable";


    public override string Category
        => CommandCategories.Calculation;

    public override string HelpMessage
        => this.BuildHelpMessage<UnSetOptions>();

    public override IArgumentCompleter? ArgumentCompleter 
        => new OptionClassCompleter<UnSetOptions>(Host);

    internal class UnSetOptions
    {
        [Value(0, HelpText = "Variable name to unset")]
        public string VariableName { get; set; }

        [Option('a', "all", HelpText = "Remove all variables")]
        public bool All { get; set; }

        public UnSetOptions()
        {
            VariableName = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<UnSetOptions>(Host);

        if (options.All)
        {
            Host.Mediator.Notify(new UnsetVariable("-+all+-"));
            return;
        }

        if (!string.IsNullOrEmpty(options.VariableName))
        {
            Host.Mediator.Notify(new UnsetVariable(options.VariableName));
            return;
        }

        Host.Output.Error("No variable name was specified");
    }
}
