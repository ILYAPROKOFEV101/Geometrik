using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Geometric
{
    using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Geometric.Figures
{
    internal class GeometricObject
    {
        // Свойства для координат
        public int X { get; private set; }
        public int Y { get; private set; }

        // Свойства для цвета
        public string ColorName { get; private set; }
        public Brush Brush { get; private set; }

        // Высота Canvas (необходима для преобразования координат)
        protected double CanvasHeight { get; private set; }

        protected Color color { get; private set; }

        // Конструктор по умолчанию
        public GeometricObject(double canvasHeight = 640) // Высота Canvas по умолчанию
        {
            SetRandomPosition();
            SetColor("Red"); // Цвет по умолчанию
            CanvasHeight = canvasHeight;
        }

        // Метод для установки случайных координат
        protected void SetRandomPosition()
        {
            Random random = new Random();
            X = random.Next(1, 700);
            Y = random.Next(1, 200); // Для декартовой системы это будет высота
        }

        // Метод для установки координат извне
        public void SetPosition(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("Координаты не могут быть отрицательными.");
            }

            X = x;
            Y = y;
        }

        // Метод для установки цвета
        public void SetColor(string colorName)
        {
            if (string.IsNullOrWhiteSpace(colorName))
                throw new ArgumentException("Название цвета не может быть пустым.");

            try
            {
                color = (Color)ColorConverter.ConvertFromString(colorName);
                ColorName = colorName;
                Brush = new SolidColorBrush(color);
            }
            catch
            {
                throw new ArgumentException($"Цвет '{colorName}' не поддерживается.");
            }
        }

        // Преобразование координаты Y для декартовой системы
        protected double TransformYCoordinate(double y)
        {
            return CanvasHeight - y;
        }

        // Вывод информации о фигуре
        public virtual string Print(string message)
        {
            return $"{message} X = {X}, Y = {Y}, цвет {ColorName}";
        }

        // Отрисовка точки на Canvas
        public virtual void Draw(Canvas canvas)
        {
            if (canvas == null)
                throw new ArgumentNullException(nameof(canvas), "Canvas не может быть null.");

            Ellipse point = new Ellipse
            {
                Width = 4, // Ширина окружности
                Height = 4, // Высота окружности
                Fill = Brush
            };

            // Устанавливаем позицию окружности
            Canvas.SetLeft(point, X - 2); // Центрируем окружность по координатам X
            Canvas.SetTop(point, TransformYCoordinate(Y) - 2); // Преобразуем координату Y

            // Добавляем точку на Canvas
            canvas.Children.Add(point);
        }
    }
}
}