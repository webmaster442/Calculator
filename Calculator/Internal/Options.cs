using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Internal;

internal sealed class Options
{
    [Description("Show hidden files")]
    public bool ShowHiddenFiles { get; set; }

    public Options()
    {
        ShowHiddenFiles = false;
    }
}