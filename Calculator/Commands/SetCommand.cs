//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;
internal sealed class SetCommand : ShellCommand
{
    public SetCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["set"];

    public override string Synopsys
        => "Sets a variable";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(2);
        var name = args.AsEnumerable().First();
        var expression = string.Join(' ', args.AsEnumerable().Skip(1));
        Host.Mediator.Notify(new SetVariable(name, expression));
    }
}
