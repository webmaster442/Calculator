using System.Text.Json;
using System.Text.Json.Nodes;

namespace Calculator.Terminal;
internal static class TerminalProfileInstaller
{
    private const string Template = "Calculator.Terminal.TerminalFragment.json";

    public static async Task<bool> Install()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Calculator.exe");
            var iconPath = Path.Combine(AppContext.BaseDirectory, "Calculator.ico");
            var targetfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Windows Terminal", "Fragments", "Calculator");
            var targetFile = Path.Combine(targetfolder, "calculator.json");

            using (var stream = typeof(TerminalProfileInstaller).Assembly.GetManifestResourceStream(Template))
            {
                if (stream == null)
                {
                    return false;
                }
                string json = await PrepareProfileAsync(stream, "Calc shell", path, iconPath);

                if (!Directory.Exists(targetfolder))
                    Directory.CreateDirectory(targetfolder);

                await File.WriteAllTextAsync(targetFile, json);

            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static async Task<string> PrepareProfileAsync(Stream fragmentTemplate,
                                                         string name,
                                                         string commandLine,
                                                         string iconPath,
                                                         CancellationToken cancellationToken = default)
    {
        var fragment = await JsonNode.ParseAsync(fragmentTemplate, cancellationToken: cancellationToken);
        JsonNode profile = fragment!["profiles"]!.AsArray().First()!;
        profile["name"] = name;
        profile["commandline"] = commandLine;
        profile["icon"] = iconPath;

        var options = new JsonSerializerOptions { WriteIndented = true };
        return fragment.ToJsonString(options);
    }
}
