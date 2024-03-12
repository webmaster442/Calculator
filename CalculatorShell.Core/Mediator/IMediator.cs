//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core.Mediator;

/// <summary>
/// Mediator interface
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Send a notification
    /// </summary>
    /// <typeparam name="TMessage">Message type</typeparam>
    /// <param name="payload">Message to send</param>
    public void Notify<TMessage>(in TMessage payload) where TMessage : PayloadBase;
    /// <summary>
    /// Request data from the first client that is able to respond to the
    /// specified request message
    /// </summary>
    /// <typeparam name="TReturn">Return type</typeparam>
    /// <typeparam name="TMessage">Request message type</typeparam>
    /// <param name="message">Request message</param>
    /// <returns>Requested data</returns>
    public TReturn? Request<TReturn, TMessage>(in TMessage message) where TMessage : PayloadBase where TReturn : class;
    /// <summary>
    /// Request data from all clients that are able to respond to the
    /// specified request message
    /// </summary>
    /// <typeparam name="TReturn">Return type</typeparam>
    /// <typeparam name="TMessage">Request message type</typeparam>
    /// <param name="message">Request message</param>
    /// <returns>Requested datas</returns>
    public IEnumerable<TReturn> RequestAll<TReturn, TMessage>(TMessage message) where TMessage : PayloadBase where TReturn : class;
    /// <summary>
    /// Register a client to the mediator
    /// </summary>
    /// <typeparam name="TClient">Client type</typeparam>
    /// <param name="client">Client instance</param>
    public void Register<TClient>(in TClient client) where TClient : IMediatorComponent;
}
