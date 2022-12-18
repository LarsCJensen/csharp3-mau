using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Point = System.Windows.Point;

namespace Assignment6
{
    public class VisualHost : FrameworkElement
    {
        private VisualCollection _children;
        int _offset = 40;
        double _yHeight;
        double _xScale;
        double _yScale;
        // TODO Are these used?
        private double _xActualSize;
        private double _yActualSize;
        // TODO Make Local
        private double _stepValueX;
        private double _stepValueY;
        public VisualHost()
        {
            _children = new VisualCollection(this);            
        }
        // Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount => _children.Count;

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }      
        
        public void DrawScale(int xMax, int xInterval, double xWidth, int yMax, int yInterval, double yHeight, string title)
        {
            _yHeight = yHeight;
            // TODO Is this used?
            _xActualSize = CalculateActualSize(xWidth, _offset);
            _yActualSize = CalculateActualSize(yHeight, _offset);
            // TODO Delete?
            _xScale = _xActualSize / xMax;
            _yScale = _yActualSize / yMax;

            int numberOfPoints = xMax / xInterval;
            _stepValueX = CalculateStepValue(numberOfPoints, _xActualSize);
            PointCollection xPoints = CalculatePointsForScale(numberOfPoints, _stepValueX, _offset, yHeight - _offset);
            numberOfPoints = yMax / yInterval;
            _stepValueY = CalculateStepValue(numberOfPoints, _yActualSize);
            PointCollection yPoints = CalculatePointsForScale(numberOfPoints, _stepValueY, _offset, yHeight - _offset, false);

            _children.Add(DrawLine(xPoints, Brushes.Black, 2));
            _children.Add(DrawLine(yPoints, Brushes.Black, 2));
            // Draw scale markers using Ellips            
            _children.Add(DrawEllipse(xPoints, Brushes.Black, 3, 3));
            _children.Add(DrawEllipse(yPoints, Brushes.Black, 3, 3));
            
            // Draw the figures on the axises
            _children.Add(DrawScaleText(xPoints, xInterval, 10, Brushes.Black));
            _children.Add(DrawScaleText(yPoints, yInterval, 10, Brushes.Black, false));

            // Draw the title 
            Point titlePoint = new Point((200 * _xScale) +_offset, (0 * _yScale));
            _children.Add(DrawDiagramTitle(title, titlePoint));
        }
        public void DrawPoints(PointCollection points)
        {
            // TODO Use calculatepoints for
            PointCollection calculatedPoints = TransformPointsToCanvas(points);
            _children.Add(DrawLine(calculatedPoints, Brushes.Black, 1));
        }
        // Helper function to calculate scale
        private double CalculateActualSize(double source, int offset)
        {
            return source - offset * 2;
        }

        private double CalculateStepValue(int numberOfPoints, double size)
        {
            return Math.Round(size / numberOfPoints);
        }
        // Helper function to calculate point collection for x and y
        private PointCollection CalculatePointsForScale(int numberOfPoints, double stepValue, int offset, double startY, bool x = true)
        {
            PointCollection points = new PointCollection();
            // Calculate stepValue based on the scale
            // We want some room in the top and bottom
            // TODO Keep this?
            //double stepValue = Math.Round(size / numberOfPoints);
            // Add starting point            
            points.Add(new Point(offset, startY));    
            for(int i = 1; i < numberOfPoints + 1; i++)
            {
                if (x == true)
                {                    
                    // For X we increase by stepvalue + offset, but keep Y at the same coordinate
                    points.Add(new Point(i * stepValue + offset, startY));
                }else
                {
                    // For Y we keep X at offset
                    points.Add(new Point(offset, startY - (i * stepValue)));
                }
            }
            return points;
        }
        private PointCollection TransformPointsToCanvas(PointCollection points)
        {
            // TODO Vi måste ha med scale
            // xMin/yMin xMax/yMax
            PointCollection transformedPoints = new PointCollection();
            foreach(Point point in points)
            {
                //double scaledXPoint = point.X * _xScale;
                //double scaledYPoint = point.Y * _yScale;
                // Use step
                transformedPoints.Add(new Point((point.X * _xScale) + _offset, _yHeight - ((point.Y * _yScale) + _offset)));
            }
            return transformedPoints;
        }
        
        public Point TransformCanvasToPoints(Point point)
        {
            // 0, 0 == top left
            // 0, 0 på skalan == 40, 330
            // 0, 0 på skalan maximerad == 40, 885
            return new Point(point.X - _offset, (_yHeight - _offset) - point.Y);
        }

        public DrawingVisual DrawLine(PointCollection points, Brush color, int size)
        {
            // TODO pass in pen
            Pen scalePen = new Pen(color, size);

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            Point lastPoint = new Point();
            foreach (Point point in points)
            {
                if (lastPoint.X.Equals(0) && lastPoint.Y.Equals(0))
                {
                    lastPoint = point;
                    continue;
                }
                context.DrawLine(scalePen, lastPoint, point);
                lastPoint = point;
            }
            context.Close();
            return visual;
        }
        public DrawingVisual DrawEllipse(PointCollection points, Brush color, int radiusX, int radiusY)
        {
            Pen scalePen = new Pen(color, 1);

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            foreach (Point point in points)
            {
                // brush, pen, center, doublex radius, double y radius
                //context.DrawEllipse(scalePen, lastPoint, point);
                context.DrawEllipse(color, scalePen, point, radiusX, radiusY);                
            }
            context.Close();
            return visual;
        }
        private DrawingVisual DrawScaleText(PointCollection points, int interval, int size, Brush color, bool x = true)
        {

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            FlowDirection direction = FlowDirection.RightToLeft;
            if (x == true)
            {
                direction = FlowDirection.LeftToRight;
            }
            

            for(int i = 0; i < points.Count;i++)
            {
                string scaleText = (interval * i).ToString();
                DrawText(ref context, scaleText, points[i], direction, size, color);
                //FlowDirection direction = FlowDirection.LeftToRight;
                //string pointString = point.X.ToString();
                //if (x == false)
                //{
                //    pointString = point.Y.ToString();
                //}

                //context.DrawText(
                //    new FormattedText(
                //        pointString, 
                //        CultureInfo.GetCultureInfo("en-us"),
                //        direction, 
                //        new Typeface("Verdana"),
                //        size,
                //        color), 
                //    point);                
            }
            context.Close();
            return visual;
        }
        private DrawingVisual DrawDiagramTitle(string title, Point point)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            FlowDirection direction = FlowDirection.LeftToRight;
            DrawText(ref context, title, point, direction, 20, Brushes.DarkRed);
            context.Close();
            return visual;

        }
        private void DrawText(ref DrawingContext context, string text, Point point, FlowDirection flowDirection, int size, Brush color)
        {
            
            context.DrawText(
                new FormattedText(
                    text,
                    CultureInfo.GetCultureInfo("en-us"),
                    flowDirection,
                    new Typeface("Verdana"),
                    size,
                    color),
                point);
        }
    }
}
