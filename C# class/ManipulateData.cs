using System.Collections.Generic;
using System.Linq;

class ManiputlateaData{
    public static void SortDataByXAscending(List<(double, double)> values){
        // Sort data in ascending order
        var sortedList = values.OrderBy(x => x.Item1).ToList();
        values.Clear();
        values.AddRange(sortedList);
    }
    public static void SortDataByYAscending(List<(double, double)> values){
        // Sort data in ascending order
        var sortedList = values.OrderBy(x => x.Item2).ToList();
        values.Clear();
        values.AddRange(sortedList);
    }
    public static void SortDataByXDescending(List<(double, double)> values){
        // Sort data in descending order
        var sortedList = values.OrderByDescending(x => x.Item1).ToList();
        values.Clear();
        values.AddRange(sortedList);
    }
    public static void SortDataByYDescending(List<(double, double)> values){
        // Sort data in descending order
        var sortedList = values.OrderByDescending(x => x.Item2).ToList();
        values.Clear();
        values.AddRange(sortedList);
    }
    public static List<(double, double)> ScaleData(List<(double, double)> values, double factor){
        // Scale data by a factor
        var scaledList=values.Select(x => (x.Item1 * factor, x.Item2 * factor)).ToList();
        values.Clear();
        values.AddRange(scaledList);
        return values;
    }
    public static List<(double, double)> RemoveDuplicates(List<(double, double)> data){
        // Remove duplicate data points
        var distinctData=data.Distinct().ToList();
        data.Clear();
        data.AddRange(distinctData);
        return data;
    }
    public static List<(double, double)> RemoveOutliers(List<(double, double)> data){
        // Remove outliers based on a threshold
         List<double> outliersX=AnalyzeData.DetectXOutliers(data);
        List<double> outliersY=AnalyzeData.DetectYOutliers(data);
        for(int i=0; i<data.Count; i++){
            if(outliersX.Contains(data[i].Item1) || outliersY.Contains(data[i].Item2)){
                data.RemoveAt(i);
                i--;
            }
        }
        return data;
    }
}