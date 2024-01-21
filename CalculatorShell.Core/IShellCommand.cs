namespace CalculatorShell.Core;

public interface IShellCommand
{
    IHost Host { get; }
    string Synopsys { get; }
    string[] Names { get; }
    IArgumentCompleter? ArgumentCompleter { get; }
}
