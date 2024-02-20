using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CalculatorShell.Core;

namespace Calculator;
internal class CurrentDirectoryProvider : ICurrentDirectoryProvider
{
    public string CurrentDirectory
    { 
        get => Environment.CurrentDirectory;
        set => Environment.CurrentDirectory = value;
    }

    public CurrentDirectoryProvider()
    {
        CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}
