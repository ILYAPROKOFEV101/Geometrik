using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Geometric;

using System;
using System.Windows;
using System.Windows.Media;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            var point = new Ellipse
            {
                Width = 4,
                Height = 4,
                Fill = Brushes.Red // Фиксированный красный цвет вместо случайного
            };

            SetRandomPosition(point);
            DrawingCanvas.Children.Add(point);
        }

        private void DrawTriangle_Click(object sender, RoutedEventArgs e)
        {
            // Генерация случайных сторон треугольника
            double a = _random.Next(50, 200);
            double b = _random.Next(50, 200);
            double c = _random.Next(50, 200);

            // Генерация случайной позиции
            double x = _random.Next(0, (int)(DrawingCanvas.ActualWidth - 50));
            double y = _random.Next(0, (int)(DrawingCanvas.ActualHeight - 50));

            // Фиксированный красный цвет
            Brush brush = Brushes.Red;

            // Создание треугольника
            Triangle triangle = new Triangle(x, y, brush, a, b, c);

            // Отрисовка треугольника
            triangle.Draw(DrawingCanvas);
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