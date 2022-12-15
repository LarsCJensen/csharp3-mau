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
            int offset = 20;
            PointCollection xPoints = CalculatePointsFor(xMax, xInterval, xWidth, yHeight, offset);
            PointCollection yPoints = CalculatePointsFor(yMax, yInterval, xWidth, yHeight, offset, false);
            _children.Add(DrawLine(xPoints, Brushes.Black, 2));
            _children.Add(DrawLine(yPoints, Brushes.Black, 2));
            // TODO DrawPoints            
            _children.Add(DrawEllipse(xPoints, Brushes.Black, 4));
            _children.Add(DrawEllipse(yPoints, Brushes.Black, 4));
            // TODO 
            _children.Add(DrawText(xPoints, 15, Brushes.Black));
            _children.Add(DrawText(yPoints, 15, Brushes.Black));
            
        }

        private PointCollection CalculatePointsFor(int max, int interval, double xWidth, double yHeight, int offset, bool x = true)
        {
            int numberOfPoints = max / interval;
            double scale = 0;
            if (x == true) 
            { 
                scale = xWidth; 
            } 
            else 
            {
                scale = yHeight;
            }

            int stepValue = (int)Math.Round(scale/numberOfPoints);
            PointCollection points = new PointCollection();            
            // Add starting point            
            points.Add(new Point(offset, offset));
            for(int i = 1; i < numberOfPoints + 1; i++)
            {
                if (x == true)
                {
                    points.Add(new Point(i * stepValue, yHeight - offset));
                }else
                {
                    points.Add(new Point(offset, scale - (i * stepValue)));
                }
            }
            return points;
        }

        public DrawingVisual DrawLine(PointCollection points, Brush color, int size)
        {
            Pen scalePen = new Pen(color, size);

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            Point lastPoint = new Point();
            // TODO Calculate xmin-xmax/xinterval     
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
        public DrawingVisual DrawEllipse(PointCollection points, Brush color, int radius)
        {
            Pen scalePen = new Pen(color, 1);

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            Point lastPoint = new Point();
            // TODO Calculate xmin-xmax/xinterval     
            foreach (Point point in points)
            {
                if (lastPoint.X.Equals(0) && lastPoint.Y.Equals(0))
                {
                    lastPoint = point;
                    continue;
                }
                context.DrawEllipse(scalePen, lastPoint, point);
                lastPoint = point;
            }
            context.Close();
            return visual;
        }
        public DrawingVisual DrawText(PointCollection points, int size, Brush color, bool x = true)
        {

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            // TODO Calculate xmin-xmax/xinterval     
            foreach (Point point in points)
            {
                FlowDirection direction = FlowDirection.LeftToRight;
                string pointString = point.X.ToString();
                if (x == false)
                {
                    pointString = point.Y.ToString();
                }

                context.DrawText(
                    new FormattedText(
                        pointString, 
                        CultureInfo.GetCultureInfo("en-us"), 
                        FlowDirection.LeftToRight, 
                        new Typeface("Verdana"),
                        size,
                        color), 
                    point);                
            }
            context.Close();
            return visual;
        }
    }
}
