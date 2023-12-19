using System.Globalization;

using CalculatorShell.Core;
using CalculatorShell.Core.Messenger;
using CalculatorShell.Engine;

using Spectre.Console;

namespace Calculator;

internal sealed class TerminalHost : IHost
{
    private readonly TerminalInput _input;
    private readonly TerminalOutput _output;

    public TerminalHost()
    {
        _input = new TerminalInput();
        _output = new TerminalOutput();
        MessageBus = new MessageBus();
    }

    public ITerminalInput Input => _input;

    public ITerminalOutput Output => _output;

    public string Prompt
    {
        get => _input.Prompt;
        set => _input.Prompt = value;
    }

    public CultureInfo CultureInfo
    {
        get => _input.CultureInfo;
        set
        {
            _input.CultureInfo = value;
            _output.CultureInfo = value;
        }
    }

    public IMessageBus MessageBus { get; }

    internal void SetComandNames(IEnumerable<string> keys, HashSet<string> exitCommands)
    {
        _input.SetCommands(keys, exitCommands);
    }

    private class TerminalOutput : ITerminalOutput
    {
        internal CultureInfo CultureInfo { get; set; }

        public TerminalOutput()
        {
            CultureInfo = CultureInfo.InvariantCulture;
        }

        public void Clear()
            => AnsiConsole.Clear();

        public void Error(Exception ex)
        {
            if (ex is ComandException or EngineException)
            {
                Error(ex.Message);
                return;
            }

            AnsiConsole.WriteException(ex);
        }

        public void Error(string message)
            => AnsiConsole.MarkupLine($"[red]{message.EscapeMarkup()}[/]");

        public void Result(string message)
            => AnsiConsole.MarkupLine($"[green]{message.EscapeMarkup()}[/]");

        public void List(string header, IEnumerable<string> data)
        {
            AnsiConsole.MarkupLine($"[green]{header.EscapeMarkup()}[/]");
            var columns = new Columns(data);
            AnsiConsole.Write(columns);
        }

        public void BlankLine()
            => AnsiConsole.WriteLine();
    }
}
