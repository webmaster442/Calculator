using System.Runtime.InteropServices;
using System.Text;

using Calculator;
using Calculator.Man;
using Calculator.Terminal;

bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
if (isWindows)
{
    bool result = await TerminalProfileInstaller.Install();
    if (!result)
    {
        Console.WriteLine("Terminal profile install failed");
        return;
    }
}

Man.RenderMan();

Console.OutputEncoding = Encoding.UTF8;

using (var app = new App())
{
    await app.Run();
}