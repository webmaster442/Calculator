using System.ComponentModel;

namespace Calculator.Internal;

internal sealed class Options
{
    [Description("Show hidden files")]
    public bool ShowHiddenFiles { get; set; }

    [Description("Split numbers by thousands")]
    public bool GroupThousands { get; set; }

    public Options()
    {
        ShowHiddenFiles = false;
    }
}