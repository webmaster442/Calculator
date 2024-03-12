//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

namespace CalculatorShell.Engine;

public interface ICalculatorFormattable
{
    string ToString(CultureInfo culture, bool thousandsGrouping);
}
