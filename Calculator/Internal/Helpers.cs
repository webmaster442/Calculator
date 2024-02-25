//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace Calculator.Internal;

internal static class Helpers
{
    public static Version GetAssemblyVersion()
    {
        return typeof(Helpers).Assembly.GetName().Version
            ?? throw new InvalidOperationException("Assembly version is not set");
    }

    public static string GetResourceString(string name)
    {
        using (var stream = typeof(Helpers).Assembly.GetManifestResourceStream(name))
        {
            if (stream == null)
                throw new InvalidOperationException($"Invalid resource name: {name}");

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public static Stream GetResourceStream(string name)
    {
        var stream = typeof(Helpers).Assembly.GetManifestResourceStream(name);

        if (stream == null)
            throw new InvalidOperationException($"Invalid resource name: {name}");

        return stream;
    }
}
