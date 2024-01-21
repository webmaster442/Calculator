using System.Text.Json;

using Calculator.Internal;
using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.AutoRun;
internal class OptionsAutoExec : IAutoExec
{
    public string LogMessage => "Loading options";

    public int Priority => int.MinValue; //first

    public void Execute(IHost host)
    {
        var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "calculator.json");
        if (!File.Exists(fileName))
        {
            host.Log.Warning($"Config file not found, progessing with defatults");
            return;
        }
        using var stream = File.OpenRead(fileName);
        var loaded = JsonSerializer.Deserialize<Options>(stream);
        if (loaded != null)
        {
            host.Mediator.Notify(new SetOptions(loaded));
        }
    }
}
