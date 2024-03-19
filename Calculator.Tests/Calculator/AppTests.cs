//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;

using NSubstitute;

namespace Calculator.Tests.Calculator;
internal class AppTests
{
    private TestTerminalInput _input;
    private TestHost _host;
    private IWritableHost _writableHost;
    private TimeProvider _timeProvider;
    private ICurrentDirectoryProvider _currentDirectoryProvider;

    private MultiLineComparer Comparer { get; set; }

    private App _sut;

    [SetUp]
    public void OneTimeSetup()
    {
        Comparer = new MultiLineComparer();
        _host = new TestHost();
        _input = new TestTerminalInput();
        _writableHost = Substitute.For<IWritableHost>();
        _timeProvider = Substitute.For<TimeProvider>();
        _ = _timeProvider.GetUtcNow().Returns(new DateTimeOffset(new DateTime(2024, 01, 01, 12, 00, 0)));
        _ = _timeProvider.LocalTimeZone.Returns(TimeZoneInfo.Utc);
        _currentDirectoryProvider = Substitute.For<ICurrentDirectoryProvider>();

        _ = _currentDirectoryProvider.CurrentDirectory.Returns(@"/test");

        _sut = new App(_host, _input, _writableHost, _timeProvider, _currentDirectoryProvider);
    }

    [TearDown]
    public void OneTimeDispose()
    {
        _sut.Dispose();
    }

    [Test]
    public async Task TestRad()
    {
        _input.InputText = "rad";
        await _sut.Run(singleRun: true);

        string expectedPrompt = """
            Calc (Rad) | 12:00
            /test >
            """;

        Assert.That(_input.Prompt.ToString(), Is.EqualTo(expectedPrompt).Using(Comparer));
    }

    [Test]
    public async Task TestGrad()
    {
        _input.InputText = "grad";
        await _sut.Run(singleRun: true);

        string expectedPrompt = """
            Calc (Grad) | 12:00
            /test >
            """;

        Assert.That(_input.Prompt.ToString(), Is.EqualTo(expectedPrompt).Using(Comparer));
    }

    [Test]
    public async Task TestDeg()
    {
        _input.InputText = "deg";
        await _sut.Run(singleRun: true);

        string expectedPrompt = """
            Calc (Deg) | 12:00
            /test >
            """;

        Assert.That(_input.Prompt.ToString(), Is.EqualTo(expectedPrompt).Using(Comparer));
    }

    [Test]
    public async Task TestBcdEncode()
    {
        _input.InputText = "bcdencode 12";
        await _sut.Run(singleRun: true);

        string expected = """
            0001 0010
            """;

        string? actual = _host.Output.ToString()?.Trim();

        Assert.That(actual, Is.EqualTo(expected).Using(Comparer));
    }

    [Test]
    public async Task TestBcdDecode()
    {
        _input.InputText = "bcddecode \"0001 0010\"";
        await _sut.Run(singleRun: true);

        string expected = """
            12
            """;

        string? actual = _host.Output.ToString()?.Trim();

        Assert.That(actual, Is.EqualTo(expected).Using(Comparer));
    }
}
