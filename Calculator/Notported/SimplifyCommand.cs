//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace Calculator.Notported;

/*internal sealed class SimplifyCommand : ShellCommand
{
    private readonly LogicEngine _engine;

    public SimplifyCommand(IHost host) : base(host)
    {
        _engine = new LogicEngine();
    }

    public override string[] Names => ["simplify"];

    public override string Synopsys
        => "Simplifies a logical expression";

    public override void ExecuteInternal(Arguments args)
    {
        args.ThrowIfNotSpecifiedAtLeast(1);

        if (args.TryParse("-v", "--variables", out int variables))
        {
            var minterms = GetMinterms(args);

            var result = _engine.Parse(variables, minterms);
            Host.Output.Result(result.ToString(Host.CultureInfo));
        }
        else
        {
            var expression = string.Join(' ', args);
            var result = _engine.Parse(expression).Simplify();
            Host.Output.Result(result.ToString(Host.CultureInfo));
        }
    }

    private static List<int> GetMinterms(Arguments args)
    {
        List<int> results = [];
        HashSet<int> skipIndex = [];
        int varIndex = args.IndexOf("-v", "--variables");
        skipIndex.Add(varIndex);
        skipIndex.Add(varIndex + 1);
        for (int i = 0; i < args.Length; i++)
        {
            if (skipIndex.Contains(i))
                continue;

            results.Add(int.Parse(args[i]));
        }
        return results;
    }
}
*/