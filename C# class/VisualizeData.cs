using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

enum ChartType
{
    Bar,
    Scatter,
    Histogram,
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
        if (OperatingSystem.IsWindowsVersionAtLeast(6, 1))
        {
            this.Text = title;
            this.WindowState = FormWindowState.Maximized;
        }
        Data = data;
        Type = type;
        Title = title;
        XLabel = xLabel;
        YLabel = yLabel;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;

        switch (Type)
        {
            case ChartType.Bar:
                RenderBarChart(g);
                break;
            case ChartType.Scatter:
                RenderScatterPlot(g);
                break;
            case ChartType.Histogram:
                RenderHistogram(g);
                break;
            case ChartType.Line:
                RenderLineChart(g);
                break;
            default:
                throw new ArgumentException("Invalid chart type");
        }
    }

    private void RenderBarChart(Graphics g)
    {
        Console.WriteLine($"Rendering Bar Chart titled '{Title}'...");
        // Add logic to draw a bar chart
    }

    private void RenderScatterPlot(Graphics g)
    {
        Console.WriteLine($"Rendering Scatter Plot titled '{Title}'...");
        foreach (var (x, y) in Data)
        {
            g.FillEllipse(Brushes.Blue, (float)x - 2, (float)y - 2, 4, 4);
        }
    }

    private void RenderHistogram(Graphics g)
    {
        Console.WriteLine($"Rendering Histogram titled '{Title}'...");
        // Add logic to draw a histogram
    }

    private void RenderLineChart(Graphics g)
    {
        Console.WriteLine($"Rendering Line Chart titled '{Title}'...");
        using (Pen pen = new Pen(Color.Blue, 2))
        {
            for (int i = 0; i < Data.Count - 1; i++)
            {
                var point1 = Data[i];
                var point2 = Data[i + 1];
                g.DrawLine(pen, (float)point1.Item1, (float)point1.Item2, (float)point2.Item1, (float)point2.Item2);
            }
        }
    }
}