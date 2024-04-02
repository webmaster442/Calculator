namespace CalculatorShell.Core;

/// <summary>
/// Provides a mechanism to do platform support check, bef
/// before loading a command.
/// </summary>
public interface IPlatformSupportCheck
{
    /// <summary>
    /// Checks if the platform supports the command
    /// </summary>
    /// <returns>true, if supported, otherwise false</returns>
    public abstract static bool IsSupported();
}
