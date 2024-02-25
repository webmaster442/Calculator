//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Reflection;

namespace CalculatorShell.Engine.Expressions;

public abstract class BaseFunction
{
    protected BaseFunction(MethodInfo methodInfo)
    {
        MethodInfo = methodInfo;
    }

    public MethodInfo MethodInfo { get; }
}
