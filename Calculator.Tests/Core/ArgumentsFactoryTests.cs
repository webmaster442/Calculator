using System.Globalization;

using CalculatorShell.Core;

namespace Calculator.Tests.Core;

[TestFixture]
internal class ArgumentsFactoryTests
{
    [Test]
    public void Test_Create_NoQutes()
    {
        var result = ArgumentsFactory.Create("foo arg1 arg2 arg3", CultureInfo.InvariantCulture);
        Assert.Multiple(() =>
        {
            Assert.That(result.cmd, Is.EqualTo("foo"));
            Assert.That(result.args.Parse<string>(0), Is.EqualTo("arg1"));
            Assert.That(result.args.Parse<string>(1), Is.EqualTo("arg2"));
            Assert.That(result.args.Parse<string>(2), Is.EqualTo("arg3"));
        });
    }

    [Test]
    public void Test_Create_WithQutes()
    {
        var result = ArgumentsFactory.Create("foo \"arg1 arg2\" arg3", CultureInfo.InvariantCulture);
        Assert.Multiple(() =>
        {
            Assert.That(result.cmd, Is.EqualTo("foo"));
            Assert.That(result.args.Parse<string>(0), Is.EqualTo("arg1 arg2"));
            Assert.That(result.args.Parse<string>(1), Is.EqualTo("arg3"));
        });
    }

}
