using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(6, 1))
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Example data
            List<(double, double)> data =  ExtractData.ParseJSON(@"C:\Users\aidan\OneDrive\Desktop\C# Final\C# class\data.json");
            data=ManiputlateaData.RemoveDuplicates(data);
            data=ManiputlateaData.RemoveOutliers(data);
            Console.WriteLine("Enter Title: ");
            String title = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter X-Axis Label: ");
            String xaxis = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Y-Axis Label: ");
            String yaxis = Console.ReadLine() ?? string.Empty;
            // Create and display the chart
            Chart chart = new(data, ChartType.Line, title, xaxis, yaxis);
            Application.Run(chart);
        }
    }
}