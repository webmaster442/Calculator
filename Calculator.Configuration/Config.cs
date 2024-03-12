//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Calculator.Configuration.Validators;

using YamlDotNet.Serialization;

namespace Calculator.Configuration;

public sealed class Config
{
    [YamlMember(Description = "Show or hide files with hidden attribute in file related commands")]
    public bool ShowHiddenFiles { get; set; }
    
    [YamlMember(Description = "Show numbers with thoudand seperators")]
    public bool ThousandGroupping { get; set; }

    [CultureValid]
    [YamlMember(Description = "Input and output display culture. Can be three or four letter iso code. Use inv for invariant")]
    public string Culture { get; set; }

    [FolderExists]
    [YamlMember(Description = "Shell startup folder")]
    public string StartFolder { get; set; }

    public Config()
    {
        Culture = CultureInfo.InvariantCulture.ThreeLetterISOLanguageName;
        StartFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}
