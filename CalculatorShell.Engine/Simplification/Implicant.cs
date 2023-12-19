namespace CalculatorShell.Engine.Simplification;

internal sealed class Implicant : IEquatable<Implicant?>
{
    public string Mask { get; set; } //number mask.
    public HashSet<int> Minterms { get; }

    public Implicant()
    {
        Mask = string.Empty;
        Minterms = new HashSet<int>(); //original integers in group.
    }

    public Implicant(string mask, IEnumerable<int> minterms)
    {
        Mask = mask;
        Minterms = minterms.ToHashSet();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Implicant);
    }

    public bool Equals(Implicant? other)
    {
        return other != null &&
               Mask == other.Mask &&
               Minterms.SequenceEqual(other.Minterms);
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(Mask);
        foreach (var item in Minterms)
        {
            hash.Add(item.GetHashCode());
        }
        return hash.ToHashCode();
    }
}
