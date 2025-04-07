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
        SideLength = 80; // Устанавливаем длину стороны по умолчанию
    }

    // Конструктор с параметрами
    public MySquare(int x, int y, string colorName, int sideLength, double canvasHeight = 400)
        : base(x, y, colorName, sideLength, canvasHeight)
    {
    }

    // Переопределение метода Draw для добавления уникального рисунка
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

        // Рисуем уникальный внутренний рисунок
        DrawUniqueInnerArt(canvas, X, (int)(TransformYCoordinate(Y) - SideLength), SideLength);
    }

    // Метод для рисования уникального внутреннего рисунка
    private void DrawUniqueInnerArt(Canvas canvas, int x, int y, int sideLength)
    {
        // Создаем круг в центре квадрата
        Ellipse circle = new Ellipse
        {
            Width = sideLength / 3,
            Height = sideLength / 3,
            Fill = new RadialGradientBrush(Colors.Yellow, Colors.Orange) // Градиентный круг
        };

        Canvas.SetLeft(circle, x + sideLength / 3); // Центр круга по X
        Canvas.SetTop(circle, y + sideLength / 3); // Центр круга по Y

        canvas.Children.Add(circle);

        // Создаем звезду в центре квадрата
        Path star = CreateStar(x + sideLength / 2, y + sideLength / 2, sideLength / 4, 5);
        canvas.Children.Add(star);

        // Создаем спираль из линий
        DrawSpiral(canvas, x, y, sideLength);
    }

    // Метод для создания звезды
    private Path CreateStar(double centerX, double centerY, double radius, int points)
    {
        PathGeometry starGeometry = new PathGeometry();
        PathFigure figure = new PathFigure();

        double angleStep = Math.PI / points;
        PointCollection pointsCollection = new PointCollection();

        for (int i = 0; i < points * 2; i++)
        {
            double length = (i % 2 == 0) ? radius : radius / 2;
            double angle = i * angleStep;
            double x = centerX + length * Math.Cos(angle);
            double y = centerY + length * Math.Sin(angle);
            pointsCollection.Add(new Point(x, y));
        }

        figure.StartPoint = pointsCollection[0];
        figure.Segments.Add(new PolyLineSegment(pointsCollection.Skip(1).ToList(), true));
        starGeometry.Figures.Add(figure);

        Path star = new Path
        {
            Data = starGeometry,
            Fill = Brushes.Gold,
            Stroke = Brushes.Black,
            StrokeThickness = 1
        };

        return star;
    }

    // Метод для рисования спирали
    private void DrawSpiral(Canvas canvas, int x, int y, int sideLength)
    {
        int steps = 50;
        double angleStep = Math.PI / 10;
        double radiusStep = sideLength / (double)steps;

        for (int i = 0; i < steps; i++)
        {
            double angle = i * angleStep;
            double radius = i * radiusStep;

            double startX = x + radius * Math.Cos(angle);
            double startY = y + radius * Math.Sin(angle);
            double endX = x + (radius + radiusStep) * Math.Cos(angle + angleStep);
            double endY = y + (radius + radiusStep) * Math.Sin(angle + angleStep);

            Line line = new Line
            {
                X1 = startX,
                Y1 = startY,
                X2 = endX,
                Y2 = endY,
                Stroke = Brushes.Cyan,
                StrokeThickness = 1
            };

            canvas.Children.Add(line);
        }
    }
}
}