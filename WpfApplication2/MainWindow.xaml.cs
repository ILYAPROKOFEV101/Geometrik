using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Geometric;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawFigure<T>() where T : GeometricObject
        {
            var color = GetRandomColor();
            int x = _random.Next(0, (int)(DrawingCanvas.ActualWidth - 50));
            int y = _random.Next(0, (int)(DrawingCanvas.ActualHeight - 50));

            GeometricObject figure;
            
            if (typeof(T) == typeof(SquareObject))
                figure = new SquareObject(x, y, color);
            else if (typeof(T) == typeof(TriangleObject))
                figure = new TriangleObject(x, y, color);
            else
                figure = new PointObject(x, y, color);

            figure.Draw(DrawingCanvas);
        }

        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            var point = new Ellipse
            {
                Width = 4,
                Height = 4,
                Fill = new SolidColorBrush(GetRandomColor())
            };

            SetRandomPosition(point);
            DrawingCanvas.Children.Add(point);
        }

        private void DrawTriangle_Click(object sender, RoutedEventArgs e)
        {
            if (DrawingCanvas.ActualWidth == 0 || DrawingCanvas.ActualHeight == 0)
                return;

            // Генерируем три случайные точки в пределах Canvas
            Point p1 = new Point(
                _random.Next(0, (int)DrawingCanvas.ActualWidth),
                _random.Next(0, (int)DrawingCanvas.ActualHeight)
            );
            Point p2 = new Point(
                _random.Next(0, (int)DrawingCanvas.ActualWidth),
                _random.Next(0, (int)DrawingCanvas.ActualHeight)
            );
            Point p3 = new Point(
                _random.Next(0, (int)DrawingCanvas.ActualWidth),
                _random.Next(0, (int)DrawingCanvas.ActualHeight)
            );

            var triangle = new Polygon
            {
                Points = new PointCollection { p1, p2, p3 },
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                Fill = Brushes.Transparent // Прозрачная заливка
            };

            DrawingCanvas.Children.Add(triangle);
        }

        private void SetRandomPosition(UIElement element)
        {
            double x = _random.Next(0, (int)(DrawingCanvas.ActualWidth - 50));
            double y = _random.Next(0, (int)(DrawingCanvas.ActualHeight - 50));
            
            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);
        }

        private Color GetRandomColor()
        {
            return Color.FromRgb(
                (byte)_random.Next(0, 255),
                (byte)_random.Next(0, 255),
                (byte)_random.Next(0, 255));
        }
    }
}