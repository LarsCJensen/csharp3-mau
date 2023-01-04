using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Utilities
{
    public static class DrawHelpers
    {
        public static DrawingVisual DrawLine(PointCollection points, Brush color, int size)
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
