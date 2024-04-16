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

    [TestCase("ABC", "++++++++[>+>++>+++>++++>+++++>++++++>+++++++>++++++++>+++++++++>++++++++++>+++++++++++>++++++++++++>+++++++++++++>++++++++++++++>+++++++++++++++>++++++++++++++++<<<<<<<<<<<<<<<<-]>>>>>>>>+.-<<<<<<<<>>>>>>>>++.--<<<<<<<<>>>>>>>>+++.---<<<<<<<<.")]
    [TestCase("99", "++++++++[>+>++>+++>++++>+++++>++++++>+++++++>++++++++>+++++++++>++++++++++>+++++++++++>++++++++++++>+++++++++++++>++++++++++++++>+++++++++++++++>++++++++++++++++<<<<<<<<<<<<<<<<-]>>>>>>>+.-<<<<<<<>>>>>>>+.-<<<<<<<.")]
    [TestCase("0", "++++++++[>+>++>+++>++++>+++++>++++++>+++++++>++++++++>+++++++++>++++++++++>+++++++++++>++++++++++++>+++++++++++++>++++++++++++++>+++++++++++++++>++++++++++++++++<<<<<<<<<<<<<<<<-]>>>>>>.<<<<<<.")]
    [TestCase("", "")]
    public void EncodeTest(string input, string expected)
    {
        var result = Brainfuck.Encode(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("Hello")]
    [TestCase("")]
    [TestCase("123456789")]
    public void EncodeInterpretRoundTripTest(string input)
    {
        var code = Brainfuck.Encode(input);
        var io = new StringBuilderBrainfuckIo();
        Brainfuck.Interpret(io, code);
        Assert.That(io.ToString().TrimEnd('\0'), Is.EqualTo(input));
    }
}
