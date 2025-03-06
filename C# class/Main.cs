using static ExtractData;
using static AnalyzeData;
using static VisualizeData;
using static ManiputlateaData;

class MainClass
{
static void Main(string[] args){
List<(double, double)> data = [(2, 2), (2, 3), (3, 4), (4, 5), (5, 6)];
Console.WriteLine(CalculateXQ1(data));
Console.WriteLine(CalculateXMedian(data));
Console.WriteLine(CalculateXQ3(data));
Console.WriteLine(XFiveNumberSummary(data));
}
}