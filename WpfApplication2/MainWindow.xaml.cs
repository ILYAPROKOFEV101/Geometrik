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
using WpfApplication2.Properties;
using WpfApplication2.Properties.Fractal;
using WpfApplication2.Properties.Myart;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            
            // Подписываемся на событие Loaded
            this.Loaded += Window_Loaded;

            // Подписываемся на событие изменения размера окна
            this.SizeChanged += Window_SizeChanged;
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Пересоздаем разметку при изменении размера окна
            CreateRuler();
        }
        
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем разметку после загрузки окна
            CreateRuler();
        }
        private void CreateRuler()
        {
            // Очищаем предыдущие линии разметки
            RulerCanvas.Children.Clear();

            // Получаем размеры DrawingCanvas
            double canvasWidth = DrawingCanvas.ActualWidth;
            double canvasHeight = DrawingCanvas.ActualHeight;

            // Шаг между линиями
            double step = 20;

            // Создаем горизонтальные линии
            for (double y = 0; y <= canvasHeight; y += step)
            {
                Line horizontalLine = new Line
                {
                    X1 = 0,
                    Y1 = y,
                    X2 = canvasWidth,
                    Y2 = y,
                    Stroke = Brushes.Black, // Цвет линий
                    StrokeThickness = 1     // Толщина линий
                };
                RulerCanvas.Children.Add(horizontalLine);
            }

            // Создаем вертикальные линии
            for (double x = 0; x <= canvasWidth; x += step)
            {
                Line verticalLine = new Line
                {
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = canvasHeight,
                    Stroke = Brushes.Black, // Цвет линий
                    StrokeThickness = 1     // Толщина линий
                };
                RulerCanvas.Children.Add(verticalLine);
            }

            // Отладочный вывод
            Console.WriteLine($"Canvas size: {canvasWidth}x{canvasHeight}");
            Console.WriteLine($"Added {RulerCanvas.Children.Count} lines to RulerCanvas.");
        }
        
        
        private SolidColorBrush selectedColorBrush;

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ColorComboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                string colorName = selectedItem.Tag.ToString();
                switch (colorName)
                {
                    case "Red":
                        selectedColorBrush = new SolidColorBrush(Colors.Red);
                        break;
                    case "Blue":
                        selectedColorBrush = new SolidColorBrush(Colors.Blue);
                        break;
                    case "Green":
                        selectedColorBrush = new SolidColorBrush(Colors.Green);
                        break;
                    default:
                        selectedColorBrush = new SolidColorBrush(Colors.Black);
                        break;
                }
            }
        }

 
        private void DrawNewArt_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли цвет
            if (selectedColorBrush == null)
            {
                MessageBox.Show("Выберите цвет перед рисованием.");
                return;
            }

            // Создаем новый квадрат
            MySquare mySquare = new MySquare(
                x: new Random().Next(10, 400), // Случайная позиция X
                y: new Random().Next(10, 200), // Случайная позиция Y
                colorName: selectedColorBrush.Color.ToString(), // Выбранный цвет
                sideLength: 150 // Длина стороны
            );

            // Рисуем квадрат с фракталом
            mySquare.Draw(DrawingCanvas);
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
            // Создаем экземпляр класса ArtDrawer
            ArtDrawer artDrawer = new ArtDrawer();

            // Вызываем метод для рисования
            artDrawer.DrawRandomArt(DrawingCanvas);
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