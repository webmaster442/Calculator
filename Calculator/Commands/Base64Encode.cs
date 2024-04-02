//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

public class Base64Encode : ShellCommand
{
    public Base64Encode(IHost host) : base(host)
    {
    }

    public override string[] Names => ["base64-encode"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Converts a string to it's base64 encoded equivalent";

    public override string HelpMessage 
        => this.BuildHelpMessage<Base64EncodeOptions>();

    internal class Base64EncodeOptions
    {
        [Value(0, HelpText = "String to encode", Required = true)]
        public string Value { get; set; }

        public Base64EncodeOptions()
        {
            Value = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<Base64EncodeOptions>(Host);

        string result = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(options.Value));

        Host.Output.Result(result);
    }
}
