using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CalculatorShell.Core;

namespace Calculator.ArgumentCompleters;
internal class DirectoryNameCompleter : IArgumentCompleter
{
    public IEnumerable<(string option, string description)> ProvideAutoCompleteItems(string text, int caret)
    {
        yield break;
    }
}
