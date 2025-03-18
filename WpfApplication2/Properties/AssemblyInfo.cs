using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Geometric
{
    public abstract class GeometricObject
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Color Color { get; protected set; }
        public Brush Brush => new SolidColorBrush(Color);

        protected GeometricObject(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public abstract void Draw(Canvas canvas);
    }
}