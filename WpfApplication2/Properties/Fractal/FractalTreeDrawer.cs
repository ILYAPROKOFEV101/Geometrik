 using System;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
 
namespace WpfApplication2.Properties.Fractal
{
   
    public class FractalTreeDrawer
    {
        private readonly Canvas _drawingCanvas;

        // Конструктор принимает Canvas, на котором будет происходить рисование
        public FractalTreeDrawer(Canvas drawingCanvas)
        {
            _drawingCanvas = drawingCanvas ?? throw new ArgumentNullException(nameof(drawingCanvas));
        }

        // Метод для рисования фрактального дерева
        public void DrawFractalTree(double x1, double y1, double length, double angle, int depth)
        {
            if (depth > 10) return; // Базовый случай: ограничиваем глубину рекурсии

            // Вычисляем конечную точку линии
            double x2 = x1 + Math.Cos(DegreesToRadians(angle)) * length;
            double y2 = y1 + Math.Sin(DegreesToRadians(angle)) * length;

            // Рисуем линию
            Line line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 2 - depth * 0.1 // Толщина уменьшается с глубиной
            };
            _drawingCanvas.Children.Add(line);

            // Рекурсивно рисуем две новые ветви
            double branchLength = length * 0.7; // Уменьшаем длину для следующих ветвей
            double leftAngle = angle - 30; // Угол для левой ветви
            double rightAngle = angle + 30; // Угол для правой ветви

            DrawFractalTree(x2, y2, branchLength, leftAngle, depth + 1);  // Левая ветвь
            DrawFractalTree(x2, y2, branchLength, rightAngle, depth + 1); // Правая ветвь
        }

        // Вспомогательный метод для преобразования градусов в радианы
        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}