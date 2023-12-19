namespace Calculator.Tests.Core.Readline;
internal static class Extensions
{
    public static ConsoleKeyInfo ToConsoleKeyInfo(this char c, Dictionary<char, Tuple<ConsoleKey, ConsoleModifiers>>? KeyCharMap = null)
    {
        // Parse the key information
        var ParsedInfo = c.ParseKeyInfo(KeyCharMap);

        // Check to see if any modifiers are pressed
        bool ctrl = ParsedInfo.Item2.HasFlag(ConsoleModifiers.Control);
        bool shift = ParsedInfo.Item2.HasFlag(ConsoleModifiers.Shift);
        bool alt = ParsedInfo.Item2.HasFlag(ConsoleModifiers.Alt);

        // Return the new instance
        return new ConsoleKeyInfo(c, ParsedInfo.Item1, shift, alt, ctrl);
    }

    private static Tuple<ConsoleKey, ConsoleModifiers> ParseKeyInfo(this char c, Dictionary<char, Tuple<ConsoleKey, ConsoleModifiers>>? KeyCharMap)
    {
        // Try to get the ConsoleKey from the character
        if (c >= char.MinValue)
        {
            bool success = Enum.TryParse(c.ToString().ToUpper(), out ConsoleKey result);
            if (success)
            {
                return Tuple.Create(result, (ConsoleModifiers)0);
            }
        }

        // Try to get the tuple of the special key character from the character map defined
        if (KeyCharMap is not null)
        {
            var result = new Tuple<ConsoleKey, ConsoleModifiers>(0, 0);
            bool success = KeyCharMap.TryGetValue(c, out result);
            if (success)
            {
                return result!;
            }
        }

        // If all else fails, return the default
        return Tuple.Create<ConsoleKey, ConsoleModifiers>(default, 0);
    }
}
