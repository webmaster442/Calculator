namespace Calculator.Internal;

internal static class Helpers
{
    public static Version GetAssemblyVersion()
    {
        return typeof(Helpers).Assembly.GetName().Version
            ?? new Version(9999, 9999, 9999, 9999);
    }
}
