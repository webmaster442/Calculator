//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Internal;
using Calculator.Resources;
using System.Text.Json.Nodes;
using System.Text.Json;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class WinterminalAdd : ShellCommand
{
    private static readonly string Targetfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                "Microsoft",
                                                                "Windows Terminal",
                                                                "Fragments",
                                                                "Calculator");

#if DEBUG
    private const string ProfileName = "calculator.debug.json";
#else
    private const string ProfileName = "calculator.json";
#endif

    public WinterminalAdd(IHost host) : base(host)
    {
    }

    public static string TerminalProfileFile
        => Path.Combine(Targetfolder, ProfileName);

    public override string[] Names => ["winterminal-add"];

    public override string Synopsys
        => "Install Calculator Shell profile to Windows terminal";

    public override void ExecuteInternal(Arguments args)
    {
        try
        {
            Install();
            Host.Output.Result("Profile installed");
        }
        catch (Exception ex)
        {
            Host.Log.Exception(ex);
            Host.Output.Error("Terminal profile install failed");
        }
    }

    private static void Install()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Calculator.exe");
        var iconPath = Path.Combine(AppContext.BaseDirectory, "Calculator.ico");
#if DEBUG
        string name = "Calc shell (dev)";
#else
            string name = "Calc shell";
#endif

        using (var stream = Helpers.GetResourceStream(ResourceNames.TerminalFragmentJson))
        {
            string json = PrepareProfile(stream, name, path, iconPath);

            if (!Directory.Exists(Targetfolder))
                Directory.CreateDirectory(Targetfolder);

            File.WriteAllText(TerminalProfileFile, json);

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
