public static class Program
{
    private static readonly Dictionary<string, Func<List<(double, double)>, string>> commands = new()
    {
        { "xmean", data => $"X Mean: {AnalyzeData.CalculateXMean(data)}" },
        { "ymean", data => $"Y Mean: {AnalyzeData.CalculateYMean(data)}" },
        { "xmedian", data => $"X Median: {AnalyzeData.CalculateXMedian(data)}" },
        { "ymedian", data => $"Y Median: {AnalyzeData.CalculateYMedian(data)}" },
        { "xq1", data => $"X Q1: {AnalyzeData.CalculateXQ1(data)}" },
        { "xq3", data => $"X Q3: {AnalyzeData.CalculateXQ3(data)}" },
        { "yq1", data => $"Y Q1: {AnalyzeData.CalculateYQ1(data)}" },
        { "yq3", data => $"Y Q3: {AnalyzeData.CalculateYQ3(data)}" },
        { "xstddev", data => $"X Standard Deviation: {AnalyzeData.CalculateXStandardDeviation(data)}" },
        { "ystddev", data => $"Y Standard Deviation: {AnalyzeData.CalculateYStandardDeviation(data)}" },
        { "correlation", data => $"Correlation: {AnalyzeData.CalculateCorrelation(data)}" },
        { "xfivenumbersummary", data => $"X Five Number Summary(max, upper quartile, median, lower quartile, min): {string.Join(", ", AnalyzeData.XFiveNumberSummary(data))}" },
        { "yfivenumbersummary", data => $"Y Five Number Summary(max, upper quartile, median, lower quartile, min): {string.Join(", ", AnalyzeData.YFiveNumberSummary(data))}" },
        { "lineofbestfit", data => $"Line of Best Fit: y={AnalyzeData.GenerateLineOfBestFit(data).Item1}x+{AnalyzeData.GenerateLineOfBestFit(data).Item2}" },
        { "sortxasc", data => $"Data sorted by X in ascending order.{ManiputlateaData.SortDataByXAscending}" },
        { "sortyasc", data => $"Data sorted by Y in ascending order.{ManiputlateaData.SortDataByYAscending}" },
        { "sortxdesc", data => $"Data sorted by X in descending order.{ManiputlateaData.SortDataByXDescending}" },
        { "sortydesc", data => $"Data sorted by Y in descending order.{ManiputlateaData.SortDataByYDescending}" },
        { "scale", data => $"Data scaled by a factor of 2.{ManiputlateaData.ScaleData(data, 2)}" },
        { "removeoutliers", data => $"Outliers removed.{ManiputlateaData.RemoveOutliers(data)}" },
        { "removeduplicates", data => $"Duplicates removed.{ManiputlateaData.RemoveDuplicates(data)}" },
        { "printdata", data => $"Data printed.{ManiputlateaData.ToString(data)}" }
        };


    [STAThread]
    public static void Main()
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(6, 1))
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            List<(double,double)> data=ExtractData.ParseJSON(@"C:\Users\aidan\OneDrive\Desktop\C# Final\C# class\data.json");
            string graphtype="";
            ChartType chartType=ChartType.Scatter;
            // Prompt the user for the graph type
            while (!graphtype.Equals("scatter", StringComparison.CurrentCultureIgnoreCase) && !graphtype.Equals("line", StringComparison.CurrentCultureIgnoreCase)&& !graphtype.Equals("boxplot", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Enter the graph type (scatter, line, boxplot): ");
                graphtype = Console.ReadLine() ?? string.Empty; 
                if (graphtype.Equals("scatter", StringComparison.CurrentCultureIgnoreCase))
                {
                    chartType = ChartType.Scatter;
                }
                else if (graphtype.Equals("line", StringComparison.CurrentCultureIgnoreCase))
                {
                    chartType = ChartType.Line;
                }
                else if (graphtype.Equals("boxplot", StringComparison.CurrentCultureIgnoreCase))
                {
                    chartType = ChartType.BoxPlot;
                }
                else
                {
                    Console.WriteLine("Invalid graph type. Please enter scatter, line, or boxplot.");
                }  
            }
            // Command loop
            // Prompts the user for commands
            string command="";
            while(!command.Equals("graph", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Enter a command. Type help to see the list of commands.");
                command = Console.ReadLine() ?? string.Empty;
                if (command.Equals("help", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("AnalyzeDataCommands:\n xmean, ymean, xmedian, ymedian, xq1, xq3, yq1, yq3, xstddev, ystddev, correlation, xfivenumbersummary, yfivenumbersummary, lineofbestfit");
                    Console.WriteLine("ManipulateDataCommands:\n sortxasc, sortyasc, sortxdesc, sortydesc, scale, removeoutliers, removeduplicates, printdata");
                    Console.WriteLine("Other Commands:\n graph, help, exit");
                }
                else if (command.Equals("graph", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("Enter Title: ");
                    string title = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter X-Axis Label: ");
                    string xaxis = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter Y-Axis Label: ");
                    string yaxis = Console.ReadLine() ?? string.Empty;
                    // Create and display the chart
                    Chart chart = new(data, chartType, title, xaxis, yaxis);
                    Application.Run(chart);
                }
                else if (commands.ContainsKey(command.ToLower()))
                {
                    Console.WriteLine(commands[command.ToLower()](data));
                }
                else if (command.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid command. Type 'help' for a list of commands.");
                }
            }
        }
    }
}