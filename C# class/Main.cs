using static ExtractData;
using static AnalyzeData;
using static VisualizeData;
using static ManiputlateaData;

class MainClass
{
static void Main(string[] args){
List<(double, double)> data = [(2, 2), (2, 2), (3, 4), (4, 5), (5, 6)];
ScaleData(data,10);
PrintData(data);
}
}