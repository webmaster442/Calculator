//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;

using Calculator.Internal;
using Calculator.Resources;

using CalculatorShell.Core;

namespace Calculator.AutoRun;

internal sealed class WindowsTerminalProfileAutoExec : IAutoExec
{
    public string LogMessage => "Checking Windows terminal profile install";

    public int Priority => 0;

    public void Execute(IHost host)
    {
        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        if (isWindows)
        {
            bool result = Install();
            if (!result)
            {
                host.Log.Error($"Terminal profile install failed");
            }
        }
        else
        {
            host.Log.Info($"skipping step, because host os is not windows");
        }
    }

    private static bool Install()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Calculator.exe");
            var iconPath = Path.Combine(AppContext.BaseDirectory, "Calculator.ico");
            var targetfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Windows Terminal", "Fragments", "Calculator");

#if DEBUG
            var targetFile = Path.Combine(targetfolder, "calculator.debug.json");
            string name = "Calc shell (dev)";
#else
            var targetFile = Path.Combine(targetfolder, "calculator.json");
            string name = "Calc shell";
#endif

            using (var stream = Helpers.GetResourceStream(ResourceNames.TerminalFragmentJson))
            {
                string json = PrepareProfile(stream, name, path, iconPath);

                if (!Directory.Exists(targetfolder))
                    Directory.CreateDirectory(targetfolder);

                File.WriteAllText(targetFile, json);

            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static string PrepareProfile(Stream fragmentTemplate,
                                         string name,
                                         string commandLine,
                                         string iconPath)
    {
        var fragment = JsonNode.Parse(fragmentTemplate);
        JsonNode profile = fragment!["profiles"]!.AsArray().First()!;
        profile["name"] = name;
        profile["commandline"] = commandLine;
        profile["icon"] = iconPath;

        var options = new JsonSerializerOptions { WriteIndented = true };
        return fragment.ToJsonString(options);
    }
}
