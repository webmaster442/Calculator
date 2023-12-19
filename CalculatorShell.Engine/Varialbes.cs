using System.Text.Json;

namespace CalculatorShell.Engine;

public class Varialbes : IVariables
{
    private readonly HashSet<string> _constants;
    private readonly Dictionary<string, Number> _variables;

    public Varialbes()
    {
        _variables = new Dictionary<string, Number>
        {
            { "e", new Number(Math.E) },
            { "pi", new Number(Math.PI) },
        };
        _constants = new HashSet<string>
        {
            "e", "pi"
        };
    }

    public IEnumerable<string> VariableNames
        => _variables.Keys;

    public Number Get(string name)
    {
        return _variables[name];
    }

    public bool IsConstant(string name)
        => _constants.Contains(name);

    public void LoadFromJson(string json)
    {
        var data = JsonSerializer.Deserialize<Dictionary<string, JsonNumber>>(json);
        if (data != null)
        {
            _variables.Clear();
            foreach (var item in data)
            {
                _variables.Add(item.Key, new Number(item.Value));
            }
            _variables["e"] = new Number(Math.E);
            _variables["pi"] = new Number(Math.PI);
        }
    }

    public string SaveToJson()
    {
        Dictionary<string, JsonNumber> result = new();
        foreach (var item in _variables)
        {
            if (_constants.Contains(item.Key))
                continue;

            result.Add(item.Key, item.Value.ToJsonNumber());
        }
        return JsonSerializer.Serialize(result);
    }

    public void Set(string name, Number value)
    {
        if (_constants.Contains(name))
            throw new EngineException($"Can't override constant {name}");

        _variables[name] = value;
    }

    public void Unset(string name)
    {
        if (_variables.ContainsKey(name))
            _variables.Remove(name);
    }
}
