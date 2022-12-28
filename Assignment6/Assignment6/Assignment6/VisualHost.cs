using System;
using System.Globalization;
using System.Security.Cryptography;
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
        // Local variables to hold the scale when transforming points
        double _xCanvasScale;
        double _yCanvasScale;
        double _xPointScale;
        double _yPointScale;
        
        private double _xActualSize;
        private double _yActualSize;
        
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
        
        public void DrawScale(double xMax, int xInterval, double xWidth, double yMax, int yInterval, double yHeight, string title)
        {
            _yHeight = yHeight;
            // Actual size of the diagram area
            _xActualSize = CalculateActualSize(xWidth, _offset);
            _yActualSize = CalculateActualSize(yHeight, _offset);
            
            // Coordinates used to convert to canvas units
            _xCanvasScale = _xActualSize / xMax;
            _yCanvasScale = _yActualSize / yMax;

            // Coordinates used to convert from canvas units
            _xPointScale = xMax / _xActualSize;
            _yPointScale = yMax / _yActualSize;

            // Value of each step in the diagram
            double _stepValueX = CalculateStepValue(xInterval, _xActualSize);
            PointCollection xPoints = CalculatePointsForScale(xInterval, _stepValueX, _offset, yHeight - _offset);
            
            double _stepValueY = CalculateStepValue(yInterval, _yActualSize);
            PointCollection yPoints = CalculatePointsForScale(yInterval, _stepValueY, _offset, yHeight - _offset, false);

            _children.Add(DrawLine(xPoints, Brushes.Black, 2));
            _children.Add(DrawLine(yPoints, Brushes.Black, 2));
            
            // Draw scale markers using Ellips            
            _children.Add(DrawEllipse(xPoints, Brushes.Black, 3, 3));
            _children.Add(DrawEllipse(yPoints, Brushes.Black, 3, 3));

            // Draw the figures on the axises
            double stepIncrementValue = xMax / xInterval;
            _children.Add(DrawScaleText(xPoints, stepIncrementValue, 10, Brushes.Black));
            stepIncrementValue = yMax / yInterval;
            _children.Add(DrawScaleText(yPoints, stepIncrementValue, 10, Brushes.Black, false));

            // Draw the title 
            Point titlePoint = new Point((_xActualSize / 2) +_offset, 10);
            _children.Add(DrawDiagramTitle(title, titlePoint));
        }
        /// <summary>
        /// Draw points based on collection
        /// </summary>
        /// <param name="points"></param>
        public void DrawPoints(PointCollection points)
        {            
            PointCollection calculatedPoints = TransformPointsToCanvas(points);
            _children.Add(DrawLine(calculatedPoints, Brushes.Black, 1));
        }
        // Helper function to calculate scale
        private double CalculateActualSize(double source, int offset)
        {
            return source - offset * 2;
        }
        // Helper function to calculate step in scale
        private double CalculateStepValue(double numberOfPoints, double size)
        {
            return size / numberOfPoints;
        }
        // Helper function to calculate point collection for x and y
        private PointCollection CalculatePointsForScale(double interval, double stepValue, int offset, double startY, bool x = true)
        {
            PointCollection points = new PointCollection();
            // Add origo point            
            points.Add(new Point(offset, startY));    
            for(int i = 1; i < interval + 1; i++)
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
        // Transforms points to canvas units
        private PointCollection TransformPointsToCanvas(PointCollection points)
        {
            PointCollection transformedPoints = new PointCollection();
            foreach(Point point in points)
            {
                transformedPoints.Add(TransformPointToCanvas(point));
            }
            return transformedPoints;
        }
        // Transfor point to canvas units
        private Point TransformPointToCanvas(Point point)
        {
            double x = (point.X * _xCanvasScale) + _offset;
            double y = _yHeight - ((point.Y * _yCanvasScale) + _offset);
            return new Point(x, y);
        }
        // Transforms canvas units to points
        public Point TransformCanvasToPoint(Point point)
        {            
            double x = (point.X - _offset) * _xPointScale ;
            double y = ((_yHeight - _offset) * _yPointScale) - (point.Y * _yPointScale);
            return new Point(x, y);
        }
        // Helper method to draw a line
        public DrawingVisual DrawLine(PointCollection points, Brush color, int size)
        {
            // FUTURE pass in pen
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
        // Helper method to draw an ellipse
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
        // Helper method to draw scale text
        private DrawingVisual DrawScaleText(PointCollection points, double stepIntervalValue, int size, Brush color, bool x = true)
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
                string scaleText = FormatDecimalString(stepIntervalValue * i);
                DrawText(ref context, scaleText, points[i], direction, size, color);                             
            }
            context.Close();
            return visual;
        }
        // Helper method to draw diagram title
        private DrawingVisual DrawDiagramTitle(string title, Point point)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            FlowDirection direction = FlowDirection.LeftToRight;
            DrawText(ref context, title, point, direction, 20, Brushes.DarkRed);
            context.Close();
            return visual;

        }
        // Helper method to draw text
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
        // TODO Move to Utilities
        private string FormatDecimalString(double value)
        {
            string s = string.Format("{0:0.00}", value);

            if (s.EndsWith("00"))
            {
                return ((int)value).ToString();
            }
            else if (s.EndsWith("0") && s != "0") {
                return s.Substring(0, s.Length - 1);
            }
            else
            {
                return s;
            }
        }
    }
}
