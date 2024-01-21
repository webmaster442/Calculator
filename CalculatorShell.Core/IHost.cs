using System.Globalization;

using CalculatorShell.Core.Mediator;

namespace CalculatorShell.Core;

public interface IHost
{
    CultureInfo CultureInfo { get; set; }
    ITerminalOutput Output { get; }
    IMediator Mediator { get; }
    IWebServices WebServices { get; }
    IDialogs Dialogs { get; }
    ILog Log { get; }
    string CurrentDirectory { get; }
}
