//The creation of the following was by claude.ai operated by Isaac Cooper
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;

class DataPoint
{
    public string Label { get; set; } = "";
    public double Value { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Simple Data Visualizer");
        Console.WriteLine("=====================");
        
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Visualize CSV file");
            Console.WriteLine("2. Visualize JSON file");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice (1-3): ");
            
            string choice = Console.ReadLine() ?? "";
            
            switch (choice)
            {
                case "1":
                    VisualizeCSV();
                    break;
                case "2":
                    VisualizeJSON();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    
    static void VisualizeCSV()
    {
        Console.WriteLine($"Current working directory: {Environment.CurrentDirectory}");
        Console.Write("Enter CSV file path: ");
        string filePath = Console.ReadLine() ?? "";
        
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found!");
            return;
        }
        
        try
        {
            //The following is code written to process the data for use of chart generation
            //Aiden has developed the csv and json processing that should be used in the final submission 
            List<DataPoint> dataPoints = ParseCSV(filePath);
            if (dataPoints.Count > 0)
            {
                GenerateAndOpenHtmlChart(dataPoints, "CSV Visualization");
            }
            else
            {
                Console.WriteLine("No valid data points found in the CSV file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    static void VisualizeJSON()
    {
        Console.Write("Enter JSON file path: ");
        string filePath = Console.ReadLine() ?? "";
        
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found!");
            return;
        }
        
        try
        {
            List<DataPoint> dataPoints = ParseJSON(filePath);
            if (dataPoints.Count > 0)
            {
                GenerateAndOpenHtmlChart(dataPoints, "JSON Visualization");
            }
            else
            {
                Console.WriteLine("No valid data points found in the JSON file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    static List<DataPoint> ParseCSV(string filePath)
    {
        List<DataPoint> dataPoints = new List<DataPoint>();
        string[] lines = File.ReadAllLines(filePath);
        
        // Skip header row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] values = line.Split(',');
            
            if (values.Length >= 2)
            {
                if (double.TryParse(values[1], out double value))
                {
                    dataPoints.Add(new DataPoint 
                    { 
                        Label = values[0], 
                        Value = value 
                    });
                }
            }
        }
        
        return dataPoints;
    }
    
    static List<DataPoint> ParseJSON(string filePath)
    {
        List<DataPoint> dataPoints = new List<DataPoint>();
        string json = File.ReadAllText(filePath);
        
        try
        {
            // Try to parse as array of objects
            var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
            
            if (data != null)
            {
                foreach (var item in data)
                {
                    var properties = item.Keys.ToList();
                    if (properties.Count >= 2)
                    {
                        string label = item[properties[0]].ToString() ?? "";
                        
                        if (double.TryParse(item[properties[1]].ToString(), out double value))
                        {
                            dataPoints.Add(new DataPoint 
                            { 
                                Label = label, 
                                Value = value 
                            });
                        }
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine("Error parsing JSON. Check the format of your file.");
        }
        
        return dataPoints;
    }
    
    static void GenerateAndOpenHtmlChart(List<DataPoint> dataPoints, string title)
    {
        StringBuilder html = new StringBuilder();
        
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html>");
        html.AppendLine("<head>");
        html.AppendLine("  <title>" + title + "</title>");
        html.AppendLine("  <script src=\"https://cdn.jsdelivr.net/npm/chart.js\"></script>");
        html.AppendLine("  <style>");
        html.AppendLine("    body { font-family: Arial, sans-serif; margin: 20px; }");
        html.AppendLine("    .chart-container { width: 800px; height: 500px; margin: 20px auto; }");
        html.AppendLine("  </style>");
        html.AppendLine("</head>");
        html.AppendLine("<body>");
        html.AppendLine("  <h1>" + title + "</h1>");
        html.AppendLine("  <div class=\"chart-container\">");
        html.AppendLine("    <canvas id=\"myChart\"></canvas>");
        html.AppendLine("  </div>");
        html.AppendLine("  <script>");
        html.AppendLine("    const ctx = document.getElementById('myChart');");
        html.AppendLine("    new Chart(ctx, {");
        html.AppendLine("      type: 'bar',");
        html.AppendLine("      data: {");
        html.AppendLine("        labels: [" + string.Join(", ", dataPoints.Select(p => "'" + p.Label.Replace("'", "\\'") + "'")) + "],");
        html.AppendLine("        datasets: [{");
        html.AppendLine("          label: 'Value',");
        html.AppendLine("          data: [" + string.Join(", ", dataPoints.Select(p => p.Value.ToString())) + "],");
        html.AppendLine("          backgroundColor: 'rgba(54, 162, 235, 0.5)',");
        html.AppendLine("          borderColor: 'rgba(54, 162, 235, 1)',");
        html.AppendLine("          borderWidth: 1");
        html.AppendLine("        }]");
        html.AppendLine("      },");
        html.AppendLine("      options: {");
        html.AppendLine("        scales: {");
        html.AppendLine("          y: {");
        html.AppendLine("            beginAtZero: true");
        html.AppendLine("          }");
        html.AppendLine("        },");
        html.AppendLine("        responsive: true,");
        html.AppendLine("        plugins: {");
        html.AppendLine("          title: {");
        html.AppendLine("            display: true,");
        html.AppendLine("            text: '" + title + "'");
        html.AppendLine("          }");
        html.AppendLine("        }");
        html.AppendLine("      }");
        html.AppendLine("    });");
        html.AppendLine("  </script>");
        html.AppendLine("</body>");
        html.AppendLine("</html>");
        
        string outputPath = Path.Combine(Environment.CurrentDirectory, "chart.html");
        File.WriteAllText(outputPath, html.ToString());
        
        Console.WriteLine($"Chart created at: {outputPath}");
        
        // Try to open the HTML file in the default browser
        try
        {
            OpenBrowser(outputPath);
            Console.WriteLine("Opening chart in your default browser...");
        }
        catch
        {
            Console.WriteLine("Could not automatically open the browser. Please open the HTML file manually.");
        }
    }
    
    static void OpenBrowser(string url)
    {
        try
        {
            // OS-specific browser launching
            if (OperatingSystem.IsMacOS())
            {
                Process.Start("open", url);
            }
            else if (OperatingSystem.IsWindows())
            {
                Process.Start("explorer", url);
            }
            else if (OperatingSystem.IsLinux())
            {
                Process.Start("xdg-open", url);
            }
        }
        catch
        {
            // If automatic opening fails, at least tell them where the file is
            Console.WriteLine($"Could not open browser automatically. Please open {url} manually.");
        }
    }
}