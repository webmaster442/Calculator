using CalculatorShell.Core;

namespace Calculator.ArgumentCompleters;

internal sealed class DelegatedCompleter : IArgumentCompleter
{
    private readonly Func<string, int, IEnumerable<(string option, string description)>> _autocompleterFunc;

    public DelegatedCompleter(Func<string,int,IEnumerable<(string option, string description)>> autocompleterFunc)
    {
        _autocompleterFunc = autocompleterFunc;
    }

    public IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        return _autocompleterFunc.Invoke(text, caret);
    }
}
