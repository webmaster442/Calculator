//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using CalculatorShell.Core.Mediator;

namespace CalculatorShell.Core;

/// <summary>
/// Command host api
/// </summary>
public interface IHost
{
    /// <summary>
    /// Current input and output culture
    /// </summary>
    CultureInfo CultureInfo { get; }
    /// <summary>
    /// Terminal output
    /// </summary>
    ITerminalOutput Output { get; }
    /// <summary>
    /// Mediator to communicate with different parts of the program
    /// </summary>
    IMediator Mediator { get; }
    /// <summary>
    /// Web services that are callable
    /// </summary>
    IWebServices WebServices { get; }
    /// <summary>
    /// User interaction dialogs
    /// </summary>
    IDialogs Dialogs { get; }
    /// <summary>
    /// Debug log
    /// </summary>
    ILog Log { get; }
    /// <summary>
    /// Current directory
    /// </summary>
    string CurrentDirectory { get; }
    /// <summary>
    /// Gets the current date and time
    /// </summary>
    /// <returns>Current date and time</returns>
    DateTime Now();
}
