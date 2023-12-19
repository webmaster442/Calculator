namespace CalculatorShell.Engine;

public interface IVariables
{
    void Set(string name, Number value);
    void Unset(string name);

    Number Get(string name);

    IEnumerable<string> VariableNames { get; }
    bool IsConstant(string name);

    void LoadFromJson(string json);
    string SaveToJson();
}
