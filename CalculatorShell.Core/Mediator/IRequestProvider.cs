//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core.Mediator;

public interface IRequestProvider<TReturn, TMessage> : IMediatorComponent where TMessage : PayloadBase where TReturn : class
{
    TReturn OnRequest(TMessage message);
}
