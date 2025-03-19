using static ExtractData;
using static AnalyzeData;
using static VisualizeData;
using static ManiputlateaData;

class MainClass
{
static void Main(string[] args){
List<(double, double)> data = [(200, 200), (200, 300), (300, 400), (400, 500), (500, 600)];
Chart chart=new ScatterPlot(data, "Scatter Plot", "X", "Y");
chart.RenderChart();
}
}