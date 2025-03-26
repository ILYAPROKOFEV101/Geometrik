using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Geometric;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

internal class Triangle : DependencyObject
{
    public static readonly DependencyProperty FirstSideProperty =
        DependencyProperty.Register("FirstSide", typeof(double), typeof(Triangle), new PropertyMetadata(0.0));

    public static readonly DependencyProperty SecondSideProperty =
        DependencyProperty.Register("SecondSide", typeof(double), typeof(Triangle), new PropertyMetadata(0.0));

    public static readonly DependencyProperty ThirdSideProperty =
        DependencyProperty.Register("ThirdSide", typeof(double), typeof(Triangle), new PropertyMetadata(0.0));

    public double FirstSide
    {
        get => (double)GetValue(FirstSideProperty);
        set => SetValue(FirstSideProperty, value);
    }

    public double SecondSide
    {
        get => (double)GetValue(SecondSideProperty);
        set => SetValue(SecondSideProperty, value);
    }

    public double ThirdSide
    {
        get => (double)GetValue(ThirdSideProperty);
        set => SetValue(ThirdSideProperty, value);
    }

    public bool IsExist { get; private set; }
    public double X { get; set; }
    public double Y { get; set; }
    public Brush Brush { get; set; }

    public Triangle(double x, double y, Brush brush, double a, double b, double c)
    {
        X = x;
        Y = y;
        Brush = brush;
        FirstSide = a;
        SecondSide = b;
        ThirdSide = c;
        CheckExistence();
    }

    private void CheckExistence()
    {
        if (FirstSide + SecondSide > ThirdSide &&
            FirstSide + ThirdSide > SecondSide &&
            SecondSide + ThirdSide > FirstSide)
        {
            IsExist = true;
        }
        else
        {
            IsExist = false;
        }
    }

    public void Draw(Canvas canvas)
    {
        if (!IsExist)
        {
            Ellipse point = new Ellipse
            {
                Width = 4,
                Height = 4,
                Fill = Brush
            };
    
            Canvas.SetLeft(point, X - 2);
            Canvas.SetTop(point, Y - 2);
            canvas.Children.Add(point);
            return;
        }
    
        // Рисуем треугольник
        PathFigure pathFigure = new PathFigure
        {
            StartPoint = new Point(X, Y)
        };
    
        pathFigure.Segments.Add(new LineSegment(new Point(X + FirstSide, Y), true));
        pathFigure.Segments.Add(new LineSegment(new Point(X + FirstSide / 2, Y - SecondSide), true));
        pathFigure.IsClosed = true;
    
        PathGeometry geometry = new PathGeometry();
        geometry.Figures.Add(pathFigure);
    
        Path triangle = new Path
        {
            Data = geometry,
            Stroke = Brush, // Используем переданный цвет
            StrokeThickness = 2 //  толщину рамки
        };
    
        canvas.Children.Add(triangle);
    }
    
    
}