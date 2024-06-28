//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Globalization;

using Calculator.Messages;

using CalculatorShell.Core.Mediator;

using OxyPlot;
using OxyPlot.Series;

namespace Calculator.Internal;

internal class PlotRendering
{
    private readonly IMediator _mediator;
    private readonly SvgExporter _exporter;
    private readonly PlotModel _model;

    public PlotRendering(IMediator mediator)
    {
        _mediator = mediator;
        _exporter = new SvgExporter
        {
            Width = 1280,
            Height = 853,
        };
        _model = new PlotModel
        {
            Background = OxyColor.FromRgb(255, 255, 255),
            Culture = CultureInfo.InvariantCulture
        };
    }

    public string Render(PlotRenderInput input)
    {
        _model.Series.Clear();
        _model.Series.Add(new LineSeries
        {
            ItemsSource = _mediator.Request<List<DataPoint>, PlotRenderInput>(input),
            Title = input.Formula
        });
        return _exporter.ExportToString(_model);
    }
}
