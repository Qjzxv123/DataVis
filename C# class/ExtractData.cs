
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.Json;

class ExtractData{
    public static List<(double,double)> ParseCSV(string filePath){
        using var reader = new StreamReader(filePath);
        List<(double, double)> list = [];
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null) continue;
            var values = line.Split(',');

            list.Add((double.Parse(values[1]), double.Parse(values[2])));
        }
        return list;
    }
    public static List<(double,double)> ParseJSON(string filePath){
        using var reader = new StreamReader(filePath);
        string json = reader.ReadToEnd();
        var data = JsonSerializer.Deserialize<List<Dictionary<string, double>>>(json);
        List<(double, double)> list = [];
        if (data != null)
        {
            foreach (var item in data)
            {
                list.Add((item["Height(Inches)"], item["Weight(Pounds)"]));
            }
        }
        return list;
    }
}


