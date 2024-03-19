//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

namespace Calculator.Configuration;

public sealed class ConfigurationEditor : ConfigHandlerBase
{
    public record class EditConfigResult(Config? Configuration, Exception? Exception);

    public async Task<EditConfigResult> EditConfig(CancellationToken cancellationToken)
    {
        using var p = new Process();
        p.StartInfo.FileName = "notepad.exe";
        p.StartInfo.Arguments = FilePath;
        _ = p.Start();
        await p.WaitForExitAsync(cancellationToken);

        try
        {
            var reader = new ConfigurationReader();
            return new EditConfigResult(reader.Deserialize(), null);
        }
        catch (Exception ex)
        {
            return new EditConfigResult(null, ex);
        }
    }
}
