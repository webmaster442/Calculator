using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorShell.Core;
public interface IHelpDataSetter
{
    void SetCommandData(IReadOnlyDictionary<string, string> commandHelps,
                        IReadOnlyDictionary<string, IArgumentCompleter> completers,
                        ISet<string> exitCommands);
}
