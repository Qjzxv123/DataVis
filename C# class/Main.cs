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
            List<(double, double)> data = new List<(double, double)>
            {
                (1, 10),
                (2, 20),
                (3, 30),
                (4, 40),
                (5, 50)
            };

            // Create and display the chart
            Chart chart = new Chart(data, ChartType.Scatter, "Line Chart Example", "X-Axis", "Y-Axis");
            Application.Run(chart);
        }
    }
}