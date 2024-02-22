using System.Globalization;
using System.Linq.Expressions;

using CalculatorShell.Engine;
using CalculatorShell.Engine.Expressions;

namespace Calculator.Tests.Engine;

[TestFixture]
internal class ArithmeticEngineTests
{
    private ArithmeticEngine _engine;
    private Varialbes _variables;

    [SetUp]
    public void Setup()
    {
        _variables = new Varialbes();
        _variables.Set("x", new Number(1.0));
        _variables.Set("y", new Number(2.0));

        _variables.Set("a", Number.FromInteger(10));
        _variables.Set("b", Number.FromInteger(15));

        _engine = new ArithmeticEngine(_variables, CultureInfo.InvariantCulture);
    }

    public static IEnumerable<TestCaseData> ValidTestCases
    {
        get
        {
            using (var stream = typeof(ArithmeticEngineTests).Assembly.GetManifestResourceStream("Calculator.Tests.ValidTestCases.txt")!)
            {
                using (var reader = new StreamReader(stream))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split('|');
                        yield return new TestCaseData(data[0].Trim(), data[1].Trim());
                    }
                }
            }
        }
    }


    [TestCaseSource(nameof(ValidTestCases))]
    [CancelAfter(1000)]
    public async Task ExecuteAsync_ReturnsNumber_WhenOk(string input, string expected)
    {
        var result = await _engine.ExecuteAsync(input, CancellationToken.None);
        result.When(number => Assert.That(number.ToString(CultureInfo.InvariantCulture), Is.EqualTo(expected)),
        exception => Assert.Fail($"{exception.Message}\r\n{exception.StackTrace}"));
    }

    [TestCaseSource(nameof(ValidTestCases))]
    public void SimplifiedExpression_Compile_ReturnsNumber_WhenOk(string input, string expected) 
    {
        var lambdaBody = _engine.Parse(input).Simplify().Compile();

        var parameters =  ExpressionFlattener.Flatten(lambdaBody).OfType<ParameterExpression>().ToArray();

        if (parameters.Length == 0)
        {
            parameters = new ParameterExpression[]
            {
                Expression.Parameter(typeof(Number)),
                Expression.Parameter(typeof(Number)),
            };
        }

        var lmbd = Expression.Lambda<Func<Number, Number, Number>>(
            lambdaBody, parameters);

        Func<Number, Number, Number> compiled = lmbd.Compile();

        var result = compiled.Invoke(new Number(1.0d), new Number(2.0d)).ToString(CultureInfo.InvariantCulture);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("")]
    [TestCase("+")]
    [TestCase("1*")]
    [TestCase("(1+1))")]
    [TestCase("((1)")]
    [TestCase("1^")]
    [TestCase("Cplx(1,1)")]
    [TestCase("Cplx(1;1)%Cplx(1;1)")]
    [TestCase("foo")]
    [CancelAfter(1000)]
    public async Task ExecuteAsync_Returns_Exception_WhenInvalid(string input)
    {
        var result = await _engine.ExecuteAsync(input, CancellationToken.None);
        result.When(number => Assert.Fail("Should have thrown"),
        exception => Assert.That(exception, Is.TypeOf<EngineException>()));
    }

    [TestCase("0/0")]
    [TestCase("y/0")]
    public void Parse_Simplify_Throws_WhenInvalid(string input)
    {
        Assert.Throws<EngineException>(() => _engine.Parse(input));
    }

    [TestCase("0/y", "0")]
    [TestCase("y/1", "y")]
    [TestCase("y/-1", "(-y)")]
    [TestCase("-x/-y", "(x / y)")]
    [TestCase("x/y", "(x / y)")]
    [TestCase("abs(3)", "3")]
    [TestCase("log(16;2)", "4")]
    [TestCase("log(16;x)", "log(16; x)")]
    [TestCase("abs(y)", "abs(y)")]
    [TestCase("2^2", "4")]
    [TestCase("x^0", "1")]
    [TestCase("x^1", "x")]
    [TestCase("0^y", "0")]
    [TestCase("x^y", "(x ^ y)")]
    [TestCase("1+1", "2")]
    [TestCase("0+y", "y")]
    [TestCase("x+0", "x")]
    [TestCase("x+-y", "(x - y)")]
    [TestCase("-x+y", "(y - x)")]
    [TestCase("x+y", "(x + y)")]
    [TestCase("3%1", "0")]
    [TestCase("0%y", "0")]
    [TestCase("y%1", "0")]
    [TestCase("x%y", "(x % y)")]
    [TestCase("0*y", "0")]
    [TestCase("1*y", "y")]
    [TestCase("-1*y", "(-y)")]
    [TestCase("3*2", "6")]
    [TestCase("x*0", "0")]
    [TestCase("x*1", "x")]
    [TestCase("x*-1", "(-x)")]
    [TestCase("-x*-y", "(x * y)")]
    [TestCase("x*y", "(x * y)")]
    [TestCase("0-y", "(-y)")]
    [TestCase("x-0", "x")]
    [TestCase("x--y", "(x + y)")]
    [TestCase("x-y", "(x - y)")]
    [TestCase("1<2", "1")]
    [TestCase("1>2", "0")]
    [TestCase("1>=2", "0")]
    [TestCase("1==2", "0")]
    [TestCase("1!=2", "1")]
    [TestCase("1<=2", "1")]
    [TestCase("x<y", "(x < y)")]
    [TestCase("x>y", "(x > y)")]
    [TestCase("x<=y", "(x <= y)")]
    [TestCase("x>=y", "(x >= y)")]
    [TestCase("x==y", "(x == y)")]
    [TestCase("x!=y", "(x != y)")]
    public void Parse_Simplifies_WhenOk(string input, string expected)
    {
        var expr = _engine.Parse(input);
        Assert.That(expr.ToString(CultureInfo.InvariantCulture), Is.EqualTo(expected));
    }
}
