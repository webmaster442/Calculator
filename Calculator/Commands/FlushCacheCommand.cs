//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class FlushCacheCommand : ShellCommand
{
    public FlushCacheCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["flushcache"];

    public override string Synopsys
        => "Invalidates & deletes cached web service results";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        Host.WebServices.FlushCache();
        Host.Output.Result("Web service cache invalidated");
    }
}