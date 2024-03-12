//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core.Mediator;

/// <summary>
/// Represents a mediator component that is able to provide data to a specific request message
/// </summary>
/// <typeparam name="TReturn">Return data type</typeparam>
/// <typeparam name="TMessage">Reqest message type</typeparam>
public interface IRequestProvider<TReturn, TMessage> : IMediatorComponent where TMessage : PayloadBase where TReturn : class
{
    /// <summary>
    /// Handles data request
    /// </summary>
    /// <param name="message">Request message</param>
    /// <returns>Return data</returns>
    TReturn OnRequest(TMessage message);
}
