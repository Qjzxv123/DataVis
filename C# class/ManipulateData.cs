class ManiputlateaData{
    public static bool SortDataByXAscending(List<(double, double)> values){
        // Sort data in ascending order
        var sortedList = values.OrderBy(x => x.Item1).ToList();
        values.Clear();
        values.AddRange(sortedList);
        return true;
    }
    public static bool SortDataByYAscending(List<(double, double)> values){
        // Sort data in ascending order
        var sortedList = values.OrderBy(x => x.Item2).ToList();
        values.Clear();
        values.AddRange(sortedList);
        return true;
    }
    public static bool SortDataByXDescending(List<(double, double)> values){
        // Sort data in descending order
        var sortedList = values.OrderByDescending(x => x.Item1).ToList();
        values.Clear();
        values.AddRange(sortedList);
        return true;
    }
    public static bool SortDataByYDescending(List<(double, double)> values){
        // Sort data in descending order
        var sortedList = values.OrderByDescending(x => x.Item2).ToList();
        values.Clear();
        values.AddRange(sortedList);
        return true;
    }
    public static List<(double,double)> ScaleData(List<(double, double)> values, double factor){
        // Scale data by a factor
        return[];
    }
    public static bool RemoveDuplicates(List<(double, double)> data){
        // Remove duplicate data points
        return true;
    }
    


}