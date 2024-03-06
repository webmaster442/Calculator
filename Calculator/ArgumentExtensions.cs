//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using CommandLine.Text;

namespace Calculator;

internal static class ArgumentExtensions
{
    public const string ArgumentHeader = "Parameters & arguments:";

    public static string BuildHelpMessage<T>(this IShellCommand cmd)
    {
        using CommandLine.Parser p = new(settings =>
        {
            settings.CaseInsensitiveEnumValues = true;
            settings.ParsingCulture = System.Globalization.CultureInfo.InvariantCulture;
            settings.GetoptMode = true;
        });

        CommandLine.ParserResult<T> result = p.ParseArguments<T>(["--help"]);
        var help = HelpText.AutoBuild(result, options =>
        {
            options.AddEnumValuesToHelpText = true;
            options.AutoVersion = false;
            options.AddDashesToOption = true;
            options.Heading = ArgumentHeader;
            options.Copyright = string.Empty;
            options.AutoHelp = false;
            return options;
        }, Console.WindowWidth);

        return help.ToString();
    }

    public static T Parse<T>(this Arguments arguments, IHost host)
    {
        using CommandLine.Parser p = new(settings =>
        {
            settings.CaseInsensitiveEnumValues = true;
            settings.ParsingCulture = host.CultureInfo;
            settings.GetoptMode = true;
        });
        CommandLine.ParserResult<T> result = p.ParseArguments<T>(arguments);
        if (result.Errors.Any())
        {
            var help = HelpText.AutoBuild(result, options =>
            {
                options.AddEnumValuesToHelpText = true;
                options.AutoVersion = false;
                options.AddDashesToOption = true;
                options.Heading = ArgumentHeader;
                options.Copyright = string.Empty;
                options.AutoHelp = false;
                return options;
            }, Console.WindowWidth);
            var msg = help.ToString();

            throw new CommandException(msg);
        }
        return result.Value;
    }
}
