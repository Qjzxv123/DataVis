using System;
using System.Collections.Generic;
using System.Linq;

class AnalyzeData{
public static double CalculateXMean(List<(double, double)> values){
    double sum = 0;
    for(int i=0; i<values.Count; i++){
        sum += values[i].Item1;
    }
    return sum/values.Count;
}
public static double CalculateYMean(List<(double, double)> values){
    double sum = 0;
    for(int i=0; i<values.Count; i++){
        sum += values[i].Item2;
    }
    return sum/values.Count;
}
public static double CalculateXStandardDeviation(List<(double, double)>  values){
    double sum = 0;
    for(int i=0; i<values.Count; i++){
        sum+=Math.Pow(values[i].Item1-CalculateXMean(values),2);
    }
    return Math.Sqrt(sum/(values.Count-1));
}
public static double CalculateYStandardDeviation(List<(double, double)>  values){
     double sum = 0;
    for(int i=0; i<values.Count; i++){
        sum+=Math.Pow(values[i].Item2-CalculateXMean(values),2);
    }
    return Math.Sqrt(sum/(values.Count-1));
}
public static double CalculateCorrelation(List<(double, double)>  values){
    double sum = 0;
    for(int i=0; i<values.Count; i++){
        sum+= (values[i].Item1-CalculateXMean(values))*(values[i].Item2-CalculateYMean(values));
    }
    return sum/(values.Count*CalculateXStandardDeviation(values)*CalculateYStandardDeviation(values));
}
public static double CalculateXMedian(List<(double, double)>  values){
    values.Sort((x, y) => x.Item1.CompareTo(y.Item1));
    if (values.Count % 2 == 0)
    {
        return (values[values.Count / 2 - 1].Item1 + values[values.Count / 2].Item1) / 2;
    }
    return values[values.Count / 2].Item1;
}
public static double CalculateYMedian(List<(double, double)>  values){
    values.Sort((x, y) => x.Item2.CompareTo(y.Item2));
    if (values.Count % 2 == 0)
    {
        return (values[values.Count / 2 - 1].Item2 + values[values.Count / 2].Item2) / 2;
    }
    return values[values.Count / 2].Item2;
}
public static double CalculateXQ1(List<(double, double)>  values){
    values.Sort((x, y) => x.Item1.CompareTo(y.Item1));
    return CalculateXMedian(values.GetRange(0, values.Count / 2));
}
public static double CalculateYQ1(List<(double, double)>  values){
    values.Sort((x, y) => x.Item2.CompareTo(y.Item2));
    return CalculateYMedian(values.GetRange(0, values.Count / 2));
}
public static double CalculateXQ3(List<(double, double)>  values){
    values.Sort((x, y) => x.Item1.CompareTo(y.Item1));
    int startIndex = (values.Count % 2 == 0) ? values.Count / 2 : (values.Count / 2) + 1;
    return CalculateXMedian(values.GetRange(startIndex, values.Count - startIndex));
}
public static double CalculateYQ3(List<(double, double)>  values){
    values.Sort((x, y) => x.Item2.CompareTo(y.Item2));
    int startIndex = (values.Count % 2 == 0) ? values.Count / 2 : (values.Count / 2) + 1;
    return CalculateYMedian(values.GetRange(startIndex, values.Count - startIndex));
}
public static double CalculateXIQR(List<(double, double)>  values){
    return CalculateXQ3(values)-CalculateXQ1(values);
}
public static double CalculateYIQR(List<(double, double)>  values){
    return CalculateYQ3(values)-CalculateYQ1(values);
}
public static String XFiveNumberSummary(List<(double, double)>  values){
    return "Min: " + values.Min(x => x.Item1) + ", Q1: " + CalculateXQ1(values) + ", Median: " + CalculateXMedian(values) + ", Q3: " + CalculateXQ3(values) + ", Max: " + values.Max(x => x.Item1);
}
public static String YFiveNumberSummary(List<(double, double)>  values){
    return "Min: " + values.Min(x => x.Item2) + ", Q1: " + CalculateYQ1(values) + ", Median: " + CalculateYMedian(values) + ", Q3: " + CalculateYQ3(values) + ", Max: " + values.Max(x => x.Item2);
}
public static List<double> DetectXOutliers(List<(double, double)> values){
    List<double> outliers=[];
    for (int i = 0; i < values.Count; i++){
        if (values[i].Item1 < CalculateXQ1(values) - 1.5 * CalculateXIQR(values) || values[i].Item1 > CalculateXQ3(values) + 1.5 * CalculateXIQR(values)){
            outliers.Add(values[i].Item1);
        }
    }
    return outliers;
}
public static List<double> DetectYOutliers(List<(double, double)> values){
    List<double> outliers = [];
    for (int i = 0; i < values.Count; i++){
        if (values[i].Item2 < CalculateYQ1(values) - 1.5 * CalculateYIQR(values) || values[i].Item2 > CalculateYQ3(values) + 1.5 * CalculateYIQR(values)){
            outliers.Add(values[i].Item2);
        }
    }
    return outliers;
}
public static string GenerateLineOfBestFit(List<(double, double)> values)
{
    double xMean = CalculateXMean(values);
    double yMean = CalculateYMean(values);
    double xStandardDeviation = CalculateXStandardDeviation(values);
    double yStandardDeviation = CalculateYStandardDeviation(values);
    double correlation = CalculateCorrelation(values);
    double slope = correlation * yStandardDeviation / xStandardDeviation;
    double yIntercept = yMean - slope * xMean;
    return "y = " + slope + "x + " + yIntercept;
}
}