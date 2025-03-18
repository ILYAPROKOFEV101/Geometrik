using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Geometric;

public class TriangleObject : GeometricObject
{
    public TriangleObject(int x, int y, Color color) : base(x, y, color) { }

    public override void Draw(Canvas canvas)
    {
        var triangle = new Polygon
        {
            Points = new PointCollection
            {
                new Point(X + 25, Y),
                new Point(X, Y + 50),
                new Point(X + 50, Y + 50)
            },
            Fill = Brush
        };

        canvas.Children.Add(triangle);
    }
}