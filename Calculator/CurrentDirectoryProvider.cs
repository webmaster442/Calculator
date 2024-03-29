//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

namespace Calculator;
internal sealed class CurrentDirectoryProvider : ICurrentDirectoryProvider
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
