namespace CalculatorShell.Core;

public interface IHelpDataSetter
{
    void SetCommandData(IReadOnlyDictionary<string, string> commandHelps,
                        IReadOnlyDictionary<string, IArgumentCompleter> completers,
                        ISet<string> exitCommands);
}
