//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using Calculator.Configuration;
using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.AutoRun;

internal sealed class LoadConfigAutoExec : IAutoExec
{
    public string LogMessage => "Loading configuration";

    public int Priority => int.MinValue; //1st

    public void Execute(IHost host, IWritableHost writableHost)
    {
        //Create default config file, if needed
        new DefaultConfigurationCreator()
            .CreateDefaultConfigIfNeeded();

        var configReader = new ConfigurationReader();

        Config config = new(); //create default;
        try
        {
            config = configReader.Deserialize();
        }
        catch (Exception ex)
        {
            host.Output.Error("Config file contains errors, using default configuration");
            host.Log.Exception(ex);
        }

        if (Directory.Exists(config.StartFolder))
            writableHost.CurrentDirectory = config.StartFolder;

        writableHost.CultureInfo = new System.Globalization.CultureInfo(config.Culture);

        host.Mediator.Notify(new SetConfig(config));
    }
}
