//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class UnSetCommand : ShellCommand
{
    public UnSetCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["unset"];

    public override string Synopsys
        => "Unset a variable";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);
        string name = args.IndexOf("-a", "--all") > 0 ? "-+all+-" : args[0];
        Host.Mediator.Notify(new UnsetVariable(name));
    }
}
