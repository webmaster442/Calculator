//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core.Mediator;

/// <summary>
/// Represents a mediator component that is able to get notifications
/// </summary>
/// <typeparam name="TMessage">Message type</typeparam>
public interface INotifyTarget<in TMessage> : IMediatorComponent where TMessage : PayloadBase
{
    /// <summary>
    /// Handles notification message
    /// </summary>
    /// <param name="message">message that need to be handled</param>
    void OnNotify(TMessage message);
}
