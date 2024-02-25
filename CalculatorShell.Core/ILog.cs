﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

public interface ILog
{
    void Exception(Exception ex);
    void Info(FormattableString text);
    void Warning(FormattableString text);
    void Error(FormattableString text);

    IEnumerable<KeyValuePair<DateTime, string>> Entries { get; }

    void Clear();
}
