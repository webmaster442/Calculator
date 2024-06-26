﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class ExpenseBallanceCommand : ShellCommand
{
    public ExpenseBallanceCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["expense-ballance"];

    public override string Category
        => CommandCategories.Expesnes;

    public override string Synopsys =>
        "Gets the current month's ballance";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        var response = Host.Mediator.Request<GetExpenseBallance, ExpenseBallanceRequest>(new ExpenseBallanceRequest())
            ?? throw new CommandException("Ballance couldn't be deremined");
        Host.Output.Result(response.Ballance.ToString(Host.CultureInfo));
    }
}
