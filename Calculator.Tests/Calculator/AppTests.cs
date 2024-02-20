using CalculatorShell.Core;

using NSubstitute;

namespace Calculator.Tests.Calculator;
internal class AppTests
{
    private TestTerminalInput _input;
    private TestHost _host;
    private IHelpDataSetter _helpDataSetter;
    private TimeProvider _timeProvider;
    private ICurrentDirectoryProvider _currentDirectoryProvider;

    private App _sut;

    [SetUp]
    public void OneTimeSetup()
    {
        _host = new TestHost();
        _input = new TestTerminalInput();
        _helpDataSetter = Substitute.For<IHelpDataSetter>();
        _timeProvider = Substitute.For<TimeProvider>();
        _timeProvider.GetUtcNow().Returns(new DateTimeOffset(new DateTime(2024, 01, 01, 12, 00, 0)));
        _timeProvider.LocalTimeZone.Returns(TimeZoneInfo.Utc);
        _currentDirectoryProvider = Substitute.For<ICurrentDirectoryProvider>();

        _currentDirectoryProvider.CurrentDirectory.Returns(@"/test");

        _sut = new App(_host, _input, _helpDataSetter, _timeProvider, _currentDirectoryProvider);
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

        Assert.That(_input.Prompt.ToString(), Is.EqualTo(expectedPrompt));
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

        Assert.That(_input.Prompt.ToString(), Is.EqualTo(expectedPrompt));
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

        Assert.That(_input.Prompt.ToString(), Is.EqualTo(expectedPrompt));
    }
}
