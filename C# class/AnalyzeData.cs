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
public static List<double> DetectXOutliers(List<(double, double)> values){
    List<double> outliers = new List<double>();

    return [];
}
public static List<double> DetectYOutliers(List<(double, double)> values){
    List<double> outliers = new List<double>();

    return [];
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