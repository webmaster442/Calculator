//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

using Calculator;

Console.OutputEncoding = Encoding.UTF8;

var dirProvider = new CurrentDirectoryProvider();

var host = new TerminalHost(dirProvider, TimeProvider.System);

using (var app = new App(host,
                         host.Input,
                         host,
                         TimeProvider.System,
                         dirProvider))
{
    await app.Run();
}