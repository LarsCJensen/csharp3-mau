using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Assignment6
{
    /// <summary>
    /// Class with Draw helper methods
    /// // TODO Move Draw functionality here
    /// </summary>
    public static class Draw
    {
        [Obsolete]
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
