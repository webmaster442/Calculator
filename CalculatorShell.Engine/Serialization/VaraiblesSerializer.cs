using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CalculatorShell.Engine.Serialization;

internal class VaraiblesSerializer
{
    private readonly JsonSerializerOptions _options;

    public VaraiblesSerializer()
    {
        _options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.WriteAsString,
            WriteIndented = true,
        };
    }

    public string Serialize(IEnumerable<KeyValuePair<string, Number>> variables)
    {
        List<JsonData> data = new();
        foreach (var variable in variables)
        {
            data.Add(CreateNew(variable.Key, variable.Value));
        }
        return JsonSerializer.Serialize(data);
    }

    public void DeserializeTo(string json, IDictionary<string,  Number> variables)
    {
        var datas = JsonSerializer.Deserialize<JsonData[]>(json);

        if (datas == null)
            return;

        foreach (var data in datas)
        {
            if (data is JsonDataInt @int)
                variables.Add(data.Name, new Number(@int.Value));
            else if (data is JsonDataDbl dbl)
                variables.Add(data.Name, new Number(dbl.Value));
            else if (data is JsonDataCplx cplx)
                variables.Add(data.Name, new Number(new System.Numerics.Complex(cplx.Real, cplx.Imaginary)));
            else if (data is JsonDataFraction fraction)
                variables.Add(data.Name, new Number(new Fraction(fraction.Numerator, fraction.Denumerator)));
            else
                throw new UnreachableException();
        }
    }

    private static JsonData CreateNew(string key, Number value)
    {
        switch (value.NumberType)
        {
            case NumberType.Integer:
                return new JsonDataInt
                {
                    Name = key,
                    Value = value.ToInt128()
                };
            case NumberType.Double:
                return new JsonDataDbl
                {
                    Name = key,
                    Value = value.ToDouble()
                };
            case NumberType.Complex:
                return new JsonDataCplx
                {
                    Name = key,
                    Real = value.ToComplex().Real,
                    Imaginary = value.ToComplex().Imaginary,
                };
            case NumberType.Fraction:
                return new JsonDataFraction
                {
                    Name = key,
                    Numerator = value.ToFraction().Numerator,
                    Denumerator = value.ToFraction().Denominator,
                };
            default:
                throw new UnreachableException();
        }
    }
}
