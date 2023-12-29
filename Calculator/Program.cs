using System.Runtime.InteropServices;

using Calculator;
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

using (var app = new App())
{
    await app.Run();
}