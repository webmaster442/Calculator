using CalculatorShell.Core;

using CommandLine.Text;

namespace Calculator;

internal static class Extensions
{
    public static T Parse<T>(this Arguments arguments, IHost host)
    {
        using CommandLine.Parser p = new CommandLine.Parser(settings =>
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
                options.Heading = string.Empty;
                options.Copyright = string.Empty;
                return options;
            }, Console.WindowWidth);
            var msg = help.ToString();

            throw new CommandException(msg);
        }
        return result.Value;
    }
}
