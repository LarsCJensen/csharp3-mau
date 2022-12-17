using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Assignment6
{
    public class VisualHost : FrameworkElement
    {
        private VisualCollection _children;
        int _offset = 40;
        double _yHeight;
        double _xScale;
        double _yScale;
        // TODO REMOVE
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
        // TODO REMOVE
        public void DrawStuff()
        {
            PointCollection points = new PointCollection();
            points.Add(new Point(50, 20));
            points.Add(new Point(80, 30));
            points.Add(new Point(120, 40));
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            Pen pen = new Pen(Brushes.Black, 5);
            Point lastPoint = new Point();
            foreach (Point point in points)
            {
                if (lastPoint.X.Equals(null))
                {
                    lastPoint = point;
                    continue;
                }
                context.DrawLine(pen, lastPoint, point);
                lastPoint = point;
            }
            context.Close();
            _children.Add(visual);
        }
        public void DrawScale(int xMax, int xInterval, double xWidth, int yMax, int yInterval, double yHeight)
        {
            _yHeight = yHeight;
            // TODO Calculate _scale
            _xActualSize = CalculateActualSize(xWidth, _offset);
            _yActualSize = CalculateActualSize(yHeight, _offset);
            // TODO Delete?
            _xScale = _xActualSize / xMax;
            _yScale = _yActualSize / yMax;

            int numberOfPoints = xMax / xInterval;
            PointCollection xPoints = CalculatePointsForScale(numberOfPoints, _xActualSize, _offset, yHeight - _offset);
            numberOfPoints = yMax / yInterval;
            PointCollection yPoints = CalculatePointsForScale(numberOfPoints, _yActualSize, _offset, yHeight - _offset, false);

            _children.Add(DrawLine(xPoints, Brushes.Black, 2));
            _children.Add(DrawLine(yPoints, Brushes.Black, 2));
            // Draw scale markers using Ellips            
            _children.Add(DrawEllipse(xPoints, Brushes.Black, 3, 3));
            _children.Add(DrawEllipse(yPoints, Brushes.Black, 3, 3));
            // TODO Should be 00
            _children.Add(DrawScaleText(xPoints, xInterval, 10, Brushes.Black));
            _children.Add(DrawScaleText(yPoints, yInterval, 10, Brushes.Black, false));            
        }
        public void DrawPoints(PointCollection points)
        {
            // TODO Use calculatepoints for
            PointCollection calculatedPoints = TransformPoints(points);
            _children.Add(DrawLine(calculatedPoints, Brushes.Black, 1));
        }
        // Helper function to calculate scale
        private double CalculateActualSize(double source, int offset)
        {
            return source - offset * 2;
        }
        // Helper function to calculate point collection for x and y
        private PointCollection CalculatePointsForScale(int numberOfPoints, double size, int offset, double startY, bool x = true)
        {
            PointCollection points = new PointCollection();
            // Calculate stepValue based on the scale
            // We want some room in the top and bottom
            double stepValue = Math.Round(size / numberOfPoints);
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
        private PointCollection TransformPoints(PointCollection points)
        {
            // TODO Vi måste ha med scale
            // xMin/yMin xMax/yMax
            PointCollection transformedPoints = new PointCollection();
            foreach(Point point in points)
            {
                //double scaledXPoint = point.X * _xScale;
                //double scaledYPoint = point.Y * _yScale;
                transformedPoints.Add(new Point(point.X + _offset, _yHeight - ((point.Y * _yScale) + _offset)));
            }
            return transformedPoints;
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
        public DrawingVisual DrawScaleText(PointCollection points, int interval, int size, Brush color, bool x = true)
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
