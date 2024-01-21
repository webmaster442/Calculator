using System.Globalization;

using CalculatorShell.Core.Messenger;

namespace CalculatorShell.Core;

public interface IHost
{
    CultureInfo CultureInfo { get; set; }
    ITerminalOutput Output { get; }
    IMessageBus MessageBus { get; }
    IWebServices WebServices { get; }
    IDialogs Dialogs { get; }
    ILog Log { get; }
}
