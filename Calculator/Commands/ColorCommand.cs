//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents.Colors;

using CommandLine;

namespace Calculator.Commands;

internal sealed class ColorCommand : ShellCommand
{
    public ColorCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["color"];

    public override string Category
        => CommandCategories.Conversions;

    public override string Synopsys
        => "Converts between color formats";

    public override string HelpMessage
        => this.BuildHelpMessage<ColorOptions>();

    internal sealed class ColorOptions
    {
        [Value(0, HelpText = "Color value to parse", Required = true)]
        public string ColorValue { get; set; }

        public ColorOptions()
        {
            ColorValue = string.Empty;
        }
    }


    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<ColorOptions>(Host);

        RGB baseRgb = GetRgb(options.ColorValue, Host.CultureInfo);

        Output(baseRgb);
    }

    private void Output(RGB baseRgb)
    {
        Host.Output.MarkupLine($"[{baseRgb.ToHex()}]████[/]");
        Host.Output.MarkupLine($"[{baseRgb.ToHex()}]████[/]");
        Host.Output.Result(baseRgb.ToString(Host.CultureInfo));
        Host.Output.Result(baseRgb.ToHsl().ToString(Host.CultureInfo));
        Host.Output.Result(baseRgb.ToYuv().ToString(Host.CultureInfo));
        Host.Output.Result(baseRgb.ToCmyk().ToString(Host.CultureInfo));
        Host.Output.Result(baseRgb.ToXyz().ToString(Host.CultureInfo));
    }

    private static RGB GetRgb(string s, IFormatProvider provider)
    {
        if (RGB.TryParse(s, provider, out RGB rgbValue))
        {
            return rgbValue;
        }
        else if (YUV.TryParse(s, provider, out YUV yuvValue))
        {
            return yuvValue.ToRgb();
        }
        else if (HSL.TryParse(s, provider, out HSL hslValue))
        {
            return hslValue.ToRgb();
        }
        else if (CMYK.TryParse(s, provider, out CMYK cmykValue))
        {
            return cmykValue.ToRgb();
        }
        else if (CieXYZ.TryParse(s, provider, out CieXYZ xyzResult))
        {
            return xyzResult.ToRgb();
        }

        throw new CommandException("Can't parse color");
    }
}
