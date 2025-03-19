using static ExtractData;
using static AnalyzeData;
using static VisualizeData;
using static ManiputlateaData;
using SkiaSharp;
class MainClass
{
static void Main(string[] args){
    int width = 800;
    int height = 600;

    using var bitmap = new SKBitmap(width, height);
    using var canvas = new SKCanvas(bitmap);
    canvas.Clear(SKColors.White);

    using var paint = new SKPaint
    {
            Color = SKColors.Blue,
            IsAntialias = true,
            StrokeWidth = 5
    };

    canvas.DrawLine(50, 50, 400, 400, paint);

    using var image = SKImage.FromBitmap(bitmap);
    using var info = image.Encode(SKEncodedImageFormat.Png, 100);
    File.WriteAllBytes("output.png", info.ToArray());

    Console.WriteLine("Image saved as output.png");

List<(double, double)> data = [(2, 2), (2, 2), (3, 4), (4, 5), (5, 6)];
ScaleData(data,10);
PrintData(data);
}
}