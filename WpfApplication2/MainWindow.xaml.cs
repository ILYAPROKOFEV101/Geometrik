using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Geometric;

using System;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Media;
using WpfApplication2.Properties.Fractal;

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

        private void Draw_CUB(object sender, RoutedEventArgs e)
        {
            // Генерируем случайную ширину и высоту для квадрата
            double size = _random.Next(20, 101); // Размер от 20 до 100

            // Создаем квадрат
            Rectangle square = new Rectangle
            {
                Width = size,               // Ширина
                Height = size,              // Высота (квадрат, поэтому ширина и высота равны)
                Fill = null,                // Нет заливки (пустой внутри)
                Stroke = Brushes.Red,       // Цвет рамки - красный
                StrokeThickness = 2         // Толщина рамки - 2 пикселя
            };


            // Устанавливаем случайную позицию для квадрата
            SetRandomPosition(square);

            // Добавляем квадрат на Canvas
            DrawingCanvas.Children.Add(square);
        }
        


        private void Draw_MyART(object sender, RoutedEventArgs e)
        {
            // Генерируем случайные координаты для центра новой фигуры
            double centerX = _random.Next(50, (int)(DrawingCanvas.ActualWidth - 50));  // Случайная позиция по X
            double centerY = _random.Next(50, (int)(DrawingCanvas.ActualHeight - 50)); // Случайная позиция по Y

            // Начальный размер первого квадрата
            double initialSize = _random.Next(100, 200); // Размер первого квадрата (случайный)

            // Рисуем вложенные квадраты с новыми параметрами
            DrawNestedSquares(centerX, centerY, initialSize, 0);
        }

        private void DrawNestedSquares(double centerX, double centerY, double size, double rotationAngle)
        {
            if (size < 10) return; // Базовый случай: если размер квадрата меньше 10, останавливаем рекурсию

            // Создаем квадрат
            Rectangle square = new Rectangle
            {
                Width = size,
                Height = size,
                Stroke = Brushes.Black,       // Цвет рамки
                StrokeThickness = 2,          // Толщина рамки
                Fill = Brushes.Transparent    // Прозрачная заливка
            };

            // Устанавливаем позицию квадрата (по центру Canvas)
            Canvas.SetLeft(square, centerX - size / 2); // Центрируем по X
            Canvas.SetTop(square, centerY - size / 2);  // Центрируем по Y

            // Поворачиваем квадрат
            RotateTransform rotateTransform = new RotateTransform(rotationAngle, size / 2, size / 2);
            square.RenderTransform = rotateTransform;

            // Добавляем квадрат на Canvas
            DrawingCanvas.Children.Add(square);

            // Рекурсивно вызываем метод для следующего квадрата
            DrawNestedSquares(centerX, centerY, size * 0.85, rotationAngle + 10); // Уменьшаем размер и увеличиваем угол поворота
        }
        
        
        private void DrawTriangle_Click(object sender, RoutedEventArgs e)
        {
            // Проверка и получение значений из текстовых полей
            if (double.TryParse(InputX.Text, out double x) &&
                double.TryParse(InputY.Text, out double y) &&
                double.TryParse(InputSideA.Text, out double sideA) &&
                double.TryParse(InputSideB.Text, out double sideB) &&
                double.TryParse(InputSideC.Text, out double sideC))
            {
                // Проверка корректности длин сторон треугольника
                if (sideA <= 0 || sideB <= 0 || sideC <= 0)
                {
                    MessageBox.Show("Длины сторон должны быть положительными числами.");
                    return;
                }

                if (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA)
                {
                    MessageBox.Show("Треугольник с такими сторонами не может существовать.");
                    return;
                }

                // Создание треугольника с заданными параметрами
                Brush brush = Brushes.Red; // Фиксированный цвет

                Triangle triangle = new Triangle(x, y, brush, sideA, sideB, sideC);

                // Отрисовка треугольника
                triangle.Draw(DrawingCanvas);
            }
            else
            {
                MessageBox.Show("Введите корректные числовые значения для координат и длин сторон.");
            }
        }

        private void Draw_FRAT(object sender, RoutedEventArgs e)
        {
            // Генерируем случайные координаты для начальной точки дерева
            double startX = _random.Next(50, (int)(DrawingCanvas.ActualWidth - 50)); // Случайная позиция X
            double startY = DrawingCanvas.ActualHeight - 50; // Начальная позиция Y (внизу Canvas)

            // Начальные параметры для дерева
            double length = _random.Next(80, 120); // Случайная начальная длина стебля
            double angle = -90; // Начальный угол (вверх)

            // Создаем экземпляр класса FractalTreeDrawer
            FractalTreeDrawer fractalTreeDrawer = new FractalTreeDrawer(DrawingCanvas);

            // Рисуем фрактальное дерево через новый класс
            fractalTreeDrawer.DrawFractalTree(startX, startY, length, angle, 0);
        }

        
        private void ClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем все дочерние элементы Canvas
            DrawingCanvas.Children.Clear();
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