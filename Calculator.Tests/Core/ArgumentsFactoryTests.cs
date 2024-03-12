//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using CalculatorShell.Core;

namespace Calculator.Tests.Core;

[TestFixture]
internal class ArgumentsFactoryTests
{
    [Test]
    public void Test_Create_NoQutes()
    {
        var result = ArgumentsFactory.Create("foo arg1 arg2 arg3");
        Assert.Multiple(() =>
        {
            Assert.That(result.cmd, Is.EqualTo("foo"));
            Assert.That(result.args[0], Is.EqualTo("arg1"));
            Assert.That(result.args[1], Is.EqualTo("arg2"));
            Assert.That(result.args[2], Is.EqualTo("arg3"));
        });
    }

    [Test]
    public void Test_Create_WithQutes()
    {
        var result = ArgumentsFactory.Create("foo \"arg1 arg2\" arg3");
        Assert.Multiple(() =>
        {
            Assert.That(result.cmd, Is.EqualTo("foo"));
            Assert.That(result.args[0], Is.EqualTo("arg1 arg2"));
            Assert.That(result.args[1], Is.EqualTo("arg3"));
        });
    }

    [Test]
    public void Test_Create_Empty()
    {
        var result = ArgumentsFactory.Create("");
        Assert.Multiple(() =>
        {
            Assert.That(result.cmd, Is.EqualTo(string.Empty));
            Assert.That(result.args.Count, Is.EqualTo(0));
        });
    }

}
