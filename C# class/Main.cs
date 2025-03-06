using static ExtractData;
using static AnalyzeData;
using static VisualizeData;
using static ManiputlateaData;

class MainClass
{
static void Main(string[] args){
Console.WriteLine(CalculateXStandardDeviation(ParseCSV(@"C:\Users\aidan\OneDrive\Desktop\C# Final\C# class\data.csv")));
}
}