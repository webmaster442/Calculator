//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core.Mediator;

public interface INotifyTarget<in TMessage> : IMediatorComponent where TMessage : PayloadBase
{
    void OnNotify(TMessage message);
}
