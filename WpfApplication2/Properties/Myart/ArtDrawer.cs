using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace WpfApplication2.Properties.Myart
{
    using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public class ArtDrawer
{
    private readonly Random _random = new Random();

    public void DrawRandomArt(Canvas drawingCanvas)
    {
        if (drawingCanvas == null)
            throw new ArgumentNullException(nameof(drawingCanvas));

        // Генерируем случайные координаты для центра новой фигуры
        double centerX = _random.Next(50, (int)(drawingCanvas.ActualWidth - 50));  // Случайная позиция по X
        double centerY = _random.Next(50, (int)(drawingCanvas.ActualHeight - 50)); // Случайная позиция по Y

        // Начальный размер первого квадрата
        double initialSize = _random.Next(100, 200); // Размер первого квадрата (случайный)

        // Рисуем вложенные квадраты с новыми параметрами
        DrawNestedSquares(drawingCanvas, centerX, centerY, initialSize, 0);
    }

    private void DrawNestedSquares(Canvas drawingCanvas, double centerX, double centerY, double size, double rotationAngle)
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
        drawingCanvas.Children.Add(square);

        // Рекурсивно вызываем метод для следующего квадрата
        DrawNestedSquares(drawingCanvas, centerX, centerY, size * 0.85, rotationAngle + 10); // Уменьшаем размер и увеличиваем угол поворота
    }
}
}