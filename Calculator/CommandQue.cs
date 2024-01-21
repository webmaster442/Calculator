﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator;
internal class CommandQue
{
    private readonly string[] _commands;
    private int _index;

    public CommandQue(string[] commands)
    {
        _commands = commands;
        _index = 0;
    }

    public string Next()
        => _commands[_index++];

    public bool HasCommands() 
        => _commands.Length > 0;
}