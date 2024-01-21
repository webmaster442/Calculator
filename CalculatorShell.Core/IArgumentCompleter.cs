namespace CalculatorShell.Core;

public interface IArgumentCompleter
{
    IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret);
}