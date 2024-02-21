﻿using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Calculator.Tests.Calculator;

internal class MultiLineComparer : IEqualityComparer<string>
{
    [return: NotNullIfNotNull(nameof(s))]
    private static string? Normalize(string? s)
    {
        if (s == null)
            return null;

        StringBuilder builder = new StringBuilder();
        foreach (var c in s)
        {
            if (c != '\r')
                builder.Append(c);
        }

        return builder.ToString();
    }

    public bool Equals(string? x, string? y)
    {
        return Normalize(x) == Normalize(y);
    }

    public int GetHashCode([DisallowNull] string obj)
    {
        return HashCode.Combine(Normalize(obj));
    }
}