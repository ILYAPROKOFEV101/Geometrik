using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Geometric;

public class SquareObject : GeometricObject
{
    public SquareObject(int x, int y, Color color) : base(x, y, color) { }

    public override void Draw(Canvas canvas)
    {
        var square = new Rectangle
        {
            Width = 50,
            Height = 50,
            Fill = Brush
        };

        Canvas.SetLeft(square, X);
        Canvas.SetTop(square, Y);
        canvas.Children.Add(square);
    }
}