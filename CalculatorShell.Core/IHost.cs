//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using CalculatorShell.Core.Mediator;

namespace CalculatorShell.Core;

public interface IHost
{
    CultureInfo CultureInfo { get; }
    ITerminalOutput Output { get; }
    IMediator Mediator { get; }
    IWebServices WebServices { get; }
    IDialogs Dialogs { get; }
    ILog Log { get; }
    string CurrentDirectory { get; }
}
