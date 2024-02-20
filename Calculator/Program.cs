using System.Text;

using Calculator;

Console.OutputEncoding = Encoding.UTF8;

var host = new TerminalHost();

using (var app = new App(host, host.Input, host, TimeProvider.System))
{
    await app.Run();
}