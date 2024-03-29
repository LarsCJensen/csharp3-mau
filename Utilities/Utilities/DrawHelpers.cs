﻿using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Utilities
{
    /// <summary>
    /// Helpers for drawing using DrawingVisual
    /// </summary>
    public static class DrawHelpers
    {
        public static DrawingVisual DrawLine(PointCollection points, Brush color, int size, bool drawPoint = true)
        {
            // FUTURE pass in pen
            Pen scalePen = new Pen(color, size);

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            // If there only is one point, draw an elipse
            if(points.Count ==1)
            {
                context.DrawEllipse(color, new Pen(color, 1), points[0], 2, 2);
            }
            Point lastPoint = new Point();
            foreach (Point point in points)
            {
                if (lastPoint.X.Equals(0) && lastPoint.Y.Equals(0))
                {
                    lastPoint = point;
                    continue;
                }
                context.DrawLine(scalePen, lastPoint, point);
                if(drawPoint == true)
                {
                    context.DrawEllipse(color, new Pen(color, 1), point, 2,2);
                }
                lastPoint = point;
            }
            context.Close();
            return visual;
        }
        public static DrawingVisual DrawStacks(PointCollection points, double startY, Brush color, int size)
        {
            // FUTURE pass in pen
            Pen scalePen = new Pen(color, size);

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            foreach (Point point in points)
            {
                context.DrawLine(scalePen, new Point(point.X, startY), point);                
            }
            context.Close();
            return visual;
        }
        public static DrawingVisual DrawEllipse(PointCollection points, Brush color, int radiusX, int radiusY)
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
        public static void DrawText(ref DrawingContext context, string text, Point point, FlowDirection flowDirection, int size, Brush color)
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
