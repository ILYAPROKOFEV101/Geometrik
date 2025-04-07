using WPF_Geometric;


using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using WPF_Geometric.WPF_Geometric.Figures;

namespace WpfApplication2.Properties
{
   
    

    internal class Square : GeometricObject
    {
        // Свойство для длины стороны квадрата
        public int SideLength { get; set; }

        // Конструктор по умолчанию
        public Square() : base()
        {
            SetRandomPosition();
            SetColor("Blue"); // Цвет по умолчанию
            SideLength = 50; // Длина стороны по умолчанию
        }

        // Конструктор с параметрами
        public Square(int x, int y, string colorName, int sideLength, double canvasHeight = 400)
            : base(canvasHeight)
        {
            SetPosition(x, y);
            SetColor(colorName);
            SetSideLength(sideLength);
        }

        // Метод для установки длины стороны
        public void SetSideLength(int sideLength)
        {
            if (sideLength <= 0)
            {
                throw new ArgumentException("Длина стороны должна быть положительным числом.");
            }

            SideLength = sideLength;
        }

        // Переопределение метода Print
        public override string Print(string message)
        {
            return $"{base.Print(message)}, длина стороны = {SideLength}";
        }

        // Переопределение метода Draw для отрисовки квадрата
        public override void Draw(Canvas canvas)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas), "Canvas не может быть null.");
            }
        

            Rectangle square = new Rectangle
            {
                Width = SideLength,
                Height = SideLength,
                Fill = Brush
            };

            // Устанавливаем позицию квадрата
            Canvas.SetLeft(square, X); // Левый верхний угол по координате X
            Canvas.SetTop(square, TransformYCoordinate(Y) - SideLength); // Преобразуем координату Y

            // Добавляем квадрат на Canvas
            canvas.Children.Add(square);
        }
    }
}