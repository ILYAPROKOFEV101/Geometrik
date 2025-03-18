using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Geometric;

public class PointObject : GeometricObject
{
    public PointObject(int x, int y, Color color) : base(x, y, color) { }

    public override void Draw(Canvas canvas)
    {
        var point = new Ellipse
        {
            Width = 10,
            Height = 10,
            Fill = Brush
        };

        Canvas.SetLeft(point, X - 5);
        Canvas.SetTop(point, Y - 5);
        canvas.Children.Add(point);
    }
}