using System.Text;

using Calculator;

Console.OutputEncoding = Encoding.UTF8;

using (var app = new App())
{
    await app.Run();
}