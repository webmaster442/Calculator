//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using CalculatorShell.Core;
using CalculatorShell.Core.Mediator;

using NSubstitute;

namespace Calculator.Tests.Calculator;

internal class TestHost : IHost
{
    public TestHost()
    {
        Output = new TestTerminalOutput();
        Mediator = new Mediator();
        WebServices = Substitute.For<IWebServices>();
        Dialogs = Substitute.For<IDialogs>();
        Log = Substitute.For<ILog>();
    }

    public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;

    public ITerminalOutput Output { get; }

    public IMediator Mediator { get; }

    public IWebServices WebServices { get; }

    public IDialogs Dialogs { get; }

    public ILog Log { get; }

    public string CurrentDirectory { get; } = @"c:\test";

    public string OutputText => Output.ToString()!;

    public DateTime Now() 
        => new DateTime(2024, 01, 04);
}
