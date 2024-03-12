//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Configuration;
using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal class ConfigCommand : ShellCommandAsync
{
    public ConfigCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["config"];

    public override string Category
        => CommandCategories.Program;

    public override string Synopsys
        => "Opens the configuration file for edit";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var editor = new ConfigurationEditor();
        var result = await editor.EditConfig(cancellationToken);
        if (result.Exception != null)
        {
            Host.Log.Exception(result.Exception);
            Host.Output.Error(result.Exception);
            return;
        }
        else if (result.Configuration != null)
        {
            Host.Mediator.Notify(new SetConfig(result.Configuration));
            Host.Output.Result("Configuration reloaded");
        }
    }
}
