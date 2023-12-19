using System.Globalization;

using CalculatorShell.Core;
using CalculatorShell.Engine;

using Spectre.Console;

namespace Calculator;

    internal class TerminalOutput : ITerminalOutput
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

        public void MarkupLine(string markup)
            => AnsiConsole.MarkupLine(markup);
    }
