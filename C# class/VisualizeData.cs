using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;
using LiveChartsCore.Defaults;
using System.Security.Cryptography.X509Certificates;

enum ChartType
{
    Scatter,
    Line,
    BoxPlot
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
            ChartType.Scatter => CreateScatterPlot(),
            ChartType.Line => CreateLineChart(),
            ChartType.BoxPlot => CreateBoxPlot(),
            _ => throw new ArgumentException("Invalid chart type")
        };

        // Add the chart control to the form
        chartControl.Dock = DockStyle.Fill;
        this.Controls.Add(chartControl);
    }

    private CartesianChart CreateScatterPlot()
    {
        return new CartesianChart
        {
            Series =
            [
                new ScatterSeries<ObservablePoint>
                {
                    Values = GetScatterValues().ToList(),
                    XToolTipLabelFormatter =(chartPoint) => $"X: {chartPoint.Coordinate.PrimaryValue}",
                    YToolTipLabelFormatter =(chartPoint) => $"Y: {chartPoint.Coordinate.SecondaryValue}"
                }
            ],
            XAxes =
            [
                new Axis { Name = XLabel }
            ],
            YAxes =
            [
                new Axis { Name = YLabel }
            ],
            ZoomMode = ZoomAndPanMode.Both // Enable zooming and panning on both axes
        };
    }

    private CartesianChart CreateLineChart()
    {

        return new CartesianChart
        {
            Series = new ISeries[]
            {
                new LineSeries<ObservablePoint>
                {
                    Values = GetLineValues().ToList(),
                    Stroke = new SolidColorPaint
                    {
                        Color = SkiaSharp.SKColors.Red,
                        StrokeThickness = 2
                    },
                    Fill = null, // No fill for the line of best fit
                    XToolTipLabelFormatter =(chartPoint) => $"X: {chartPoint.Coordinate.SecondaryValue}",
                    YToolTipLabelFormatter =(chartPoint) => $"Y: {chartPoint.Coordinate.PrimaryValue}"
                }
                
            },
            XAxes =
            [
                new Axis { Name = XLabel }
            ],
            YAxes =
            [
                new Axis { Name = YLabel }
            ],
            ZoomMode = ZoomAndPanMode.Both // Enable zooming and panning on both axes
        };
    }

    private CartesianChart CreateBoxPlot(){
        return new CartesianChart
        {
        Series = new List<ISeries>
        {
            new BoxSeries<BoxValue>
            {
                Name = XLabel,
                Values = new List<BoxValue>
                {
                    new BoxValue(
                        AnalyzeData.XFiveNumberSummary(Data)[0],
                        AnalyzeData.XFiveNumberSummary(Data)[1],
                        AnalyzeData.XFiveNumberSummary(Data)[2],
                        AnalyzeData.XFiveNumberSummary(Data)[3],
                        AnalyzeData.XFiveNumberSummary(Data)[4])
                }
            },
            new BoxSeries<BoxValue>
            {
                Name = YLabel,
                Values = new List<BoxValue>
                {
                    new BoxValue(
                        AnalyzeData.YFiveNumberSummary(Data)[0],
                        AnalyzeData.YFiveNumberSummary(Data)[1],
                        AnalyzeData.YFiveNumberSummary(Data)[2],
                        AnalyzeData.YFiveNumberSummary(Data)[3],
                        AnalyzeData.YFiveNumberSummary(Data)[4])
                }
            }
        },
            XAxes =
            [
                new Axis { Name = XLabel }
            ],
            YAxes =
            [
                new Axis { Name = YLabel }
            ],
            ZoomMode = ZoomAndPanMode.Both // Enable zooming and panning on both axes
        };
    }
    private IEnumerable<ObservablePoint> GetScatterValues()
    {
        foreach (var (x, y) in Data)
        {
            yield return new ObservablePoint(x, y);
        }
    }
    private IEnumerable<ObservablePoint> GetLineValues()
    {
        (double Slope, double Intercept) bestFitLine = AnalyzeData.GenerateLineOfBestFit(Data);
        for(int i=-100; i<=100; i++){
        yield return new ObservablePoint(i, bestFitLine.Slope * i + bestFitLine.Intercept);
    }
    }
}