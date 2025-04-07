using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication2.Properties
{
    internal class MySquare : Square
{
    // Конструктор по умолчанию
    public MySquare() : base()
    {
        SetColor("Purple"); // Устанавливаем цвет по умолчанию
        SideLength = 150; // Устанавливаем длину стороны по умолчанию
    }

    // Конструктор с параметрами
    public MySquare(int x, int y, string colorName, int sideLength, double canvasHeight = 400)
        : base(x, y, colorName, sideLength, canvasHeight)
    {
    }

    // Переопределение метода Draw для добавления фрактала
    public override void Draw(Canvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas), "Canvas не может быть null.");
        }

        // Отрисовка внешнего квадрата
        Rectangle square = new Rectangle
        {
            Width = SideLength,
            Height = SideLength,
            Fill = Brushes.Transparent, // Прозрачный фон для видимости внутреннего рисунка
            Stroke = Brush, // Граница квадрата
            StrokeThickness = 2
        };

        // Устанавливаем позицию квадрата
        Canvas.SetLeft(square, X); // Левый верхний угол по координате X
        Canvas.SetTop(square, TransformYCoordinate(Y) - SideLength); // Преобразуем координату Y

        // Добавляем квадрат на Canvas
        canvas.Children.Add(square);

        // Рисуем фрактал
        DrawFractal(canvas, X, (int)(TransformYCoordinate(Y) - SideLength), SideLength, 4);
    }

    // Метод для рисования фрактала
    private void DrawFractal(Canvas canvas, int x, int y, int size, int depth)
    {
        if (depth == 0)
        {
            return; // Базовый случай: прекращаем рекурсию
        }

        // Рисуем текущий квадрат
        Rectangle fractalSquare = new Rectangle
        {
            Width = size,
            Height = size,
            Fill = Brushes.Transparent, // Прозрачный фон
            Stroke = Brush, // Граница квадрата
            StrokeThickness = 1
        };

        Canvas.SetLeft(fractalSquare, x);
        Canvas.SetTop(fractalSquare, y);

        canvas.Children.Add(fractalSquare);

        // Рекурсивно рисуем 4 меньших квадрата
        int newSize = size / 3; // Размер следующего уровня фрактала
        DrawFractal(canvas, x + newSize, y, newSize, depth - 1);                    // Верхний центральный квадрат
        DrawFractal(canvas, x, y + newSize, newSize, depth - 1);                    // Левый центральный квадрат
        DrawFractal(canvas, x + newSize * 2, y + newSize, newSize, depth - 1);      // Правый центральный квадрат
        DrawFractal(canvas, x + newSize, y + newSize * 2, newSize, depth - 1);      // Нижний центральный квадрат
    }
}
}