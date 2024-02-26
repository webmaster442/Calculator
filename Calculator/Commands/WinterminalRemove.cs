//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class WinterminalRemove : ShellCommand
{
    public WinterminalRemove(IHost host) : base(host)
    {
    }

    public override string[] Names => ["winterminal-remove"];

    public override string Synopsys
        => "Remove Calculator Shell profile from Windows terminal";

    public override void ExecuteInternal(Arguments args)
    {
        try
        {
            if (File.Exists(WinterminalAdd.TerminalProfileFile))
            {
                File.Delete(WinterminalAdd.TerminalProfileFile);
                Host.Output.Result("Profile removed");
            }
            else
            {
                Host.Output.Result("Profile isn't installed");
            }
        }
        catch (Exception ex)
        {
            Host.Log.Exception(ex);
            Host.Output.Error("Terminal profile remove failed");
        }
    }
}