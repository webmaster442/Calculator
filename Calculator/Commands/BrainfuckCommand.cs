using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

using CommandLine;

namespace Calculator.Commands;
internal sealed class BrainfuckCommand : ShellCommandAsync, IBrainFuckIO
{
    public BrainfuckCommand(IHost host) : base(host)
    {
    }

    public override string[] Names
        => ["brainfuck"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "A Brainfuck interpreter";

    public override string HelpMessage
        => this.BuildHelpMessage<BrainfuckOptions>();


    internal class BrainfuckOptions
    {
        [Value(0, HelpText = "File name or brainfuck string to execute")]
        public string Data { get; set; }

        [Option('s', "string", HelpText = "Treats input as string, istead of file")]
        public bool IsString { get; set; }

        public BrainfuckOptions()
        {
            Data = string.Empty;
        }
    }

    byte IBrainFuckIO.Read()
        => Host.Dialogs.ReadChar();

    void IBrainFuckIO.Print(byte c)
        => Host.Output.Write(c);

    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        var options = args.Parse<BrainfuckOptions>(Host);
        if (options.IsString)
        {
            if (string.IsNullOrEmpty(options.Data))
            {
                throw new CommandException("No input is specified");
            }

            Brainfuck.Interpret(this, options.Data);
            return;
        }

        string code = string.Empty; 

        if (!string.IsNullOrEmpty(options.Data))
        {
            code = await File.ReadAllTextAsync(options.Data, cancellationToken);
        }
        else
        {
            var name = await Host.Dialogs.SelectFile(cancellationToken);
            code = await File.ReadAllTextAsync(name, cancellationToken);
        }

        Brainfuck.Interpret(this, code);
    }
}