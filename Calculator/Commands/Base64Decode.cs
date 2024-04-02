//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using CommandLine;

namespace Calculator.Commands;

public class Base64Decode : ShellCommand
{
    public Base64Decode(IHost host) : base(host)
    {
    }

    public override string[] Names => ["base64-decode"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Converts a base64 encoded string to it's original form";

    public override string HelpMessage 
        => this.BuildHelpMessage<Base64DecodeOptions>();

    internal class Base64DecodeOptions
    {
        [Value(0, HelpText = "String to decode", Required = true)]
        public string Value { get; set; }

        public Base64DecodeOptions()
        {
            Value = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<Base64DecodeOptions>(Host);

        string result = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(options.Value));

        Host.Output.Result(result);
    }
}