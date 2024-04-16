using CalculatorShell.Engine.MathComponents;

namespace Calculator.Tests.MathComponents;

[TestFixture]
internal class BrainfuckTests
{
    [TestCase("+++++++[>+++++++>+++++>+++++++<<<-]>-.>---.>.<.>+.<.>++.<.>++++.<.<+.>>--.<.<++.-.>.>.--.<.<-.+.++++++.>.>--.+++.+.<.>-.----.+.<.>-.-.++.++.", "0 1 2 4 8 16 32 64 128 256 512 1024")]
    [TestCase("+++++++[>+++++++<-]>.+.+.+.+.+.+.+.+.", "123456789")]
    [TestCase("++++++++[>++++++++<-]>+.+.+.+.", "ABCD")]
    [TestCase("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.", "Hello World!\n")]
    public void InterpretTest(string code, string expected)
    {
        var io = new StringBuilderBrainfuckIo();
        Brainfuck.Interpret(io, code);
        Assert.That(io.ToString(), Is.EqualTo(expected));
    }
}
