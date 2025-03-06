using System.Reflection.PortableExecutable;
class Chart{

}
class Point:Chart{

}
class VisualizeData{
    public static Chart CreateBarChart(List<(double, double)> data){
        return new Chart();
    }
    public static Chart CreateScatterPlot(List<(double, double)> data){
        return new Chart();
    }
    public static Chart CreateHistogram(List<(double, double)> data){
        return new Chart();
    }
    public static Chart CreateBoxPlot(List<(double, double)> data){
        return new Chart();
    }
    public static Chart CreatePieChart(List<(double, double)> data){
        return new Chart();
    }
    public static Chart CreateLineChart(List<(double, double)> data){
        return new Chart();
    }
    public static void RenderChart(Chart chart){

    }
 
}