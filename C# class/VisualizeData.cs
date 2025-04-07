using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Measure;
using LiveChartsCore.Defaults;

enum ChartType
{
    Bar,
    Scatter,
    Line
}

class Chart : Form
{
    private List<(double, double)> Data { get; set; }
    private string Title { get; set; }
    private string XLabel { get; set; }
    private string YLabel { get; set; }
    private ChartType Type { get; set; }

    public Chart(List<(double, double)> data, ChartType type, string title = "Untitled", string xLabel = "Untitled", string yLabel = "Untitled")
    {
        Data = data;
        Type = type;
        Title = title;
        XLabel = xLabel;
        YLabel = yLabel;

        InitializeChart();
    }

    private void InitializeChart()
    {
        this.Text = Title;
        this.Width = Screen.PrimaryScreen?.Bounds.Width ?? 800; // Default to 800 if PrimaryScreen is null
        this.Height = Screen.PrimaryScreen?.Bounds.Height ?? 600;
        this.WindowState = FormWindowState.Maximized;

        // Create the chart control based on the chart type
        Control chartControl = Type switch
        {
            ChartType.Bar => CreateBarChart(),
            ChartType.Scatter => CreateScatterPlot(),
            ChartType.Line => CreateLineChart(),
            _ => throw new ArgumentException("Invalid chart type")
        };

        // Add the chart control to the form
        chartControl.Dock = DockStyle.Fill;
        this.Controls.Add(chartControl);
    }

    private CartesianChart CreateBarChart()
    {
        return new CartesianChart
        {
            Series = new ISeries[]
            {
                new ColumnSeries<double>
                {
                    Values = GetYValues().ToList()
                }
            },
            XAxes = new Axis[]
            {
                new Axis { Name = XLabel }
            },
            YAxes = new Axis[]
            {
                new Axis { Name = YLabel }
            },
            ZoomMode = ZoomAndPanMode.Both // Enable zooming and panning on both axes
        };
    }

    private CartesianChart CreateScatterPlot()
    {
        return new CartesianChart
        {
            Series = new ISeries[]
            {
                new ScatterSeries<ObservablePoint>
                {
                    Values = GetScatterValues().ToList()
                }
            },
            XAxes = new Axis[]
            {
                new Axis { Name = XLabel }
            },
            YAxes = new Axis[]
            {
                new Axis { Name = YLabel }
            },
            ZoomMode = ZoomAndPanMode.Both // Enable zooming and panning on both axes
        };
    }

    private CartesianChart CreateLineChart()
    {
        return new CartesianChart
        {
            Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = GetYValues().ToList()
                }
            },
            XAxes = new Axis[]
            {
                new Axis { Name = XLabel }
            },
            YAxes = new Axis[]
            {
                new Axis { Name = YLabel }
            },
            ZoomMode = ZoomAndPanMode.Both // Enable zooming and panning on both axes
        };
    }

    private IEnumerable<double> GetYValues()
    {
        foreach (var (_, y) in Data)
        {
            yield return y;
        }
    }

    private IEnumerable<ObservablePoint> GetScatterValues()
    {
        foreach (var (x, y) in Data)
        {
            yield return new ObservablePoint(x, y);
        }
    }
}