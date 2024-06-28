//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using CalculatorShell.Core.Mediator;

namespace Calculator.Messages;

internal sealed class PlotRenderInput : PayloadBase
{
    [Required]
    public string Formula { get; set; }
    [Required]
    public double Start { get; set; }
    [Required]
    public double End { get; set; }
    [Required]
    public double Step { get; set; }

    public PlotRenderInput()
    {
        Formula = string.Empty;
    }
}
