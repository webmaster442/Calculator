using System.Text;

using Calculator;

Console.OutputEncoding = Encoding.UTF8;

var dirProvider = new CurrentDirectoryProvider();

var host = new TerminalHost(dirProvider);

using (var app = new App(host,
                         host.Input,
                         host,
                         TimeProvider.System,
                         dirProvider))
{
    await app.Run();
}