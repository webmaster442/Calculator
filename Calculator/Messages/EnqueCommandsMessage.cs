﻿using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;
internal class EnqueCommandsMessage : PayloadBase
{
    public EnqueCommandsMessage(string[] commands)
    {
        Commands = commands;
    }

    public string[] Commands { get; }
}
