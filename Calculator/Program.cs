using System.Text;

using Calculator;
using Calculator.Man;


Man.RenderMan();

Console.OutputEncoding = Encoding.UTF8;

using (var app = new App())
{
    await app.Run();
}