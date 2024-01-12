using CalculatorShell.Core.Messenger;

namespace Calculator.Messages;

internal sealed class CurrentDirMessage : MessageBase
{
    public CurrentDirMessage(Guid sender, string folder) : base(sender)
    {
        CurrentFolder = folder;
    }

    public string CurrentFolder { get; set; }
}
