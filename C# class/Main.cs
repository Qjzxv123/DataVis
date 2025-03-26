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
            List<(double, double)> data = [(200, 200), (200, 300), (300, 400), (400, 500), (500, 600)];

            Console.Write("Enter chart title: ");
            string title = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter X-axis label: ");
            string xAxisLabel = Console.ReadLine()?? string.Empty;
            Console.Write("Enter Y-axis label: ");
            string yAxisLabel = Console.ReadLine()?? string.Empty;

            Chart chart=new Chart(data, ChartType.Scatter, title, xAxisLabel, yAxisLabel);
            Application.Run(chart);
        }
    }
}