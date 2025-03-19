using System.Runtime.InteropServices;
using SkiaSharp;

class Chart(List<(double, double)> data, string type, string title = "Untitled", string xLabel = "Untitled", string yLabel = "Untitled")
{
    
    public string Title { get; set; } = title;
    public string XLabel { get; set; } = xLabel;
    public string YLabel { get; set; } = yLabel;
    public List<(double, double)> Data { get; set; } = data;
    public string Type { get; set; } = type;

    public virtual void RenderChart()
    {
        Console.WriteLine($"Rendering {Type} chart titled '{Title}' with XLabel '{XLabel}' and YLabel '{YLabel}'.");
    }
}

class BarChart(List<(double, double)> data, string title = "Untitled", string xLabel = "Untitled", string yLabel = "Untitled") : Chart(data, "Bar", title, xLabel, yLabel)
{
    public override void RenderChart()
    {
        Console.WriteLine($"Rendering Bar Chart titled '{Title}'...");
        // Add specific rendering logic for Bar Chart
    }
}

class ScatterPlot(List<(double, double)> data, string title = "Untitled", string xLabel = "Untitled", string yLabel = "Untitled") : Chart(data, "Scatter", title, xLabel, yLabel)
{
    public override void RenderChart()
    {
        using var bitmap = new SKBitmap(1000, 1000);
        using var canvas = new SKCanvas(bitmap);
        Console.WriteLine($"Rendering Scatter Plot titled '{Title}'...");
        foreach (var (x, y) in Data)
        {
            canvas.DrawPoint(new SKPoint((float)x, (float)y), new SKPaint()
            {
                Color = SKColors.Black,
                StrokeWidth = 10
            });
        }
        using var image=SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        File.WriteAllBytes("../../../scatter.png", data.ToArray());
    }
}

class Histogram(List<(double, double)> data, string title = "Untitled", string xLabel = "Untitled", string yLabel = "Untitled") : Chart(data, "Histogram", title, xLabel, yLabel)
{
    public override void RenderChart()
    {
        Console.WriteLine($"Rendering Histogram titled '{Title}'...");
        // Add specific rendering logic for Histogram
    }
}

class PieChart(List<(double, double)> data, string title = "Untitled", string xLabel = "Untitled", string yLabel = "Untitled") : Chart(data, "Pie", title, xLabel, yLabel)
{
    public override void RenderChart()
    {
        Console.WriteLine($"Rendering Pie Chart titled '{Title}'...");
        // Add specific rendering logic for Pie Chart
    }
}

class LineChart(List<(double, double)> data, string title = "Untitled", string xLabel = "Untitled", string yLabel = "Untitled") : Chart(data, "Line", title, xLabel, yLabel)
{
    public override void RenderChart()
    {
        Console.WriteLine($"Rendering Line Chart titled '{Title}'...");
        // Add specific rendering logic for Line Chart
    }
}

class VisualizeData
{
    public static void PrintData(List<(double, double)> data)
    {
        foreach (var (x, y) in data)
        {
            Console.WriteLine($"X: {x}, Y: {y}");
        }
    }

    public static Chart CreateChart(string type, List<(double, double)> data, string title = "Untitled", string xLabel = "Untitled", string yLabel = "Untitled")
    {
        return type.ToLower() switch
        {
            "bar" => new BarChart(data, title, xLabel, yLabel),
            "scatter" => new ScatterPlot(data, title, xLabel, yLabel),
            "histogram" => new Histogram(data, title, xLabel, yLabel),
            "pie" => new PieChart(data, title, xLabel, yLabel),
            "line" => new LineChart(data, title, xLabel, yLabel),
            _ => throw new ArgumentException("Invalid chart type")
        };
    }
    

    
}