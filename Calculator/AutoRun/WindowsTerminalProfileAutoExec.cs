//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Runtime.InteropServices;

using Calculator.Commands;

using CalculatorShell.Core;

namespace Calculator.AutoRun;

internal sealed class WindowsTerminalProfileAutoExec : IAutoExec
{
    public string LogMessage => "Checking Windows terminal profile install";

    public int Priority => int.MaxValue; //last

    public void Execute(IHost host)
    {
        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        if (isWindows && !IsInstalled())
        {
            host.Output.BlankLine();
            host.Output.MarkupLine("[yellow]Windows terminal profile can be installed with the winterminal-add command[/]");
            host.Output.BlankLine();
        }
    }

    private static bool IsInstalled()
        => File.Exists(WinterminalAdd.TerminalProfileFile);
}
