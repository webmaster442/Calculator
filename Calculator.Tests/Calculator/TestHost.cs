//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;

using NSubstitute;

namespace Calculator.Tests.Calculator;

internal sealed class TestHost : IHost
{
    public TestHost()
    {
        Output = new TestTerminalOutput();
        Mediator = new Mediator();
        WebServices = Substitute.For<IWebServices>();
        Dialogs = Substitute.For<IDialogs>();
        Log = Substitute.For<IStructuredLog>();
    }

    public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;

    public ITerminalOutput Output { get; }

    public IMediator Mediator { get; }

    public IWebServices WebServices { get; }

    public IDialogs Dialogs { get; }

    public IStructuredLog Log { get; }

    public string CurrentDirectory { get; } = @"c:\test";

    public string OutputText => Output.ToString()!;

    public DateTime Now()
        => new(2024, 01, 04);
}
