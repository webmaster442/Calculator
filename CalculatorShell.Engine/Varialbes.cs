//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text.Json;

namespace CalculatorShell.Engine;

public class Varialbes : IVariables
{
    private readonly Dictionary<string, Number> _variables;
    private readonly Dictionary<string, Number> _constants;

    public Varialbes()
    {
        _variables = new Dictionary<string, Number>();
        _constants = new Dictionary<string, Number>
        {
            { "e",          new Number(Math.E) },
            { "pi",         new Number(Math.PI) },
            { "quetta",     new Number(1E30) },
            { "ronna",      new Number(1E27) },
            { "yotta",      new Number(1E24) },
            { "zetta",      new Number(1E21) },
            { "exa",        new Number(1E18) },
            { "peta",       new Number(1E15) },
            { "tera",       new Number(1E12) },
            { "giga",       new Number(1E9) },
            { "mega",       new Number(1E6) },
            { "kilo",       new Number(1E3) },
            { "hecto",      new Number(1E2) },
            { "deca",       new Number(1E1) },
            { "deci",       new Number(1E-1) },
            { "centi",      new Number(1E-2) },
            { "milli",      new Number(1E-3) },
            { "micro",      new Number(1E-6) },
            { "nano",       new Number(1E-9) },
            { "pico",       new Number(1E-12) },
            { "femto",      new Number(1E-15) },
            { "atto",       new Number(1E-18) },
            { "zepto",      new Number(1E-21) },
            { "yocto",      new Number(1E-24) },
            { "ronto",      new Number(1E-27) },
            { "quecto",     new Number(1E-30) },
            { "kib",        new Number((Int128)Constants.KiB) },
            { "mib",        new Number((Int128)Constants.MiB) },
            { "gib",        new Number((Int128)Constants.GiB) },
            { "tib",        new Number((Int128)Constants.TiB) },
            { "pib",        new Number((Int128)Constants.PiB) },
            { "eib",        new Number((Int128)Constants.EiB) },
        };
    }
    public IEnumerable<string> VariableNames
        => _variables.Keys;

    public IEnumerable<KeyValuePair<string, Number>> AsEnumerable()
        => _variables;

    public Number Get(string name)
    {
        if (_constants.ContainsKey(name))
            return _constants[name];

        if (!_variables.ContainsKey(name))
            throw new EngineException($"Variable hasn't been set: {name}");

        return _variables[name];
    }

    public bool IsConstant(string name)
        => _constants.Keys.Contains(name);

    public void LoadFromJson(string json)
    {
        var data = JsonSerializer.Deserialize<Dictionary<string, JsonNumber>>(json);
        if (data != null)
        {
            _variables.Clear();
            foreach (var item in data)
            {
                if (IsConstant(item.Key))
                    continue;

                _variables.Add(item.Key, new Number(item.Value));
            }
        }
    }

    public string SaveToJson()
    {
        Dictionary<string, JsonNumber> result = new();
        foreach (var item in _variables)
        {
            result.Add(item.Key, item.Value.ToJsonNumber());
        }
        return JsonSerializer.Serialize(result);
    }

    public void Set(string name, Number value)
    {
        if (IsConstant(name))
            throw new EngineException($"Can't override constant: {name}");

        _variables[name] = value;
    }

    public void Unset(string name)
    {
        if (IsConstant(name))
            throw new EngineException($"Can't unset constant: {name}");

        if (_variables.ContainsKey(name))
            _variables.Remove(name);
    }

    public void UnsetAll() 
        => _variables.Clear();
}
