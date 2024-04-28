//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

internal abstract class HashCommandbase : ShellCommandAsync, IProgress<long>
{
    protected HashCommandbase(IHost host) : base(host)
    {
    }

    public override string Category
        => CommandCategories.Hash;

    internal sealed class HashOptions
    {
        [Value(0, HelpText = "File name or string to hash")]
        public string Data { get; set; }

        [Option('s', "string", HelpText = "Treats input as string, istead of file")]
        public bool IsString { get; set; }

        public HashOptions()
        {
            Data = string.Empty;
        }
    }

    public override string HelpMessage
        => this.BuildHelpMessage<HashOptions>();

    public virtual void Report(long value)
    {
        Host.Output.Write($"Processed: {value} bytes\r");
    }
}
