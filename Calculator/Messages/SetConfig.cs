//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Configuration;

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class SetConfig : PayloadBase
{
    public SetConfig(Config config)
    {
        Config = config;
    }

    public Config Config { get; }
}