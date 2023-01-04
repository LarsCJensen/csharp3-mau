using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Utilities;

namespace LoveYourBudget.Diagram
{
    public class VisualHost: FrameworkElement
    {
        private VisualCollection _children;
        int _offset = 40;
        // TODO REMOVE
        //double _yHeight;
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
        public void DrawScale(double xMax, int xInterval, double xWidth, double startX, double yMax, int yInterval, double yHeight, double startY)
        {
            // TODO
            //_yHeight = yHeight;
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
            // Ugly workaround in case someone uses max value of 1
            double numberOfPoints;
            if (xMax == 1)
            {
                numberOfPoints = xInterval;
            }
            else
            {
                numberOfPoints = xMax / xInterval;
            }

            double _stepValueX = CalculateStepValue(Math.Round(numberOfPoints), _xActualSize);
            PointCollection xPoints = CalculatePointsForScale(numberOfPoints, _stepValueX, _offset, yHeight - _offset);

            if (yMax == 1)
            {
                numberOfPoints = yInterval;
            }
            else
            {
                numberOfPoints = yMax / yInterval;
            }

            double _stepValueY = CalculateStepValue(Math.Round(numberOfPoints), _yActualSize);
            PointCollection yPoints = CalculatePointsForScale(numberOfPoints, _stepValueY, _offset, yHeight - _offset, false);

            _children.Add(DrawHelpers.DrawLine(xPoints, Brushes.Black, 2));
            _children.Add(DrawHelpers.DrawLine(yPoints, Brushes.Black, 2));

            // Draw scale markers using Ellips            
            _children.Add(DrawHelpers.DrawEllipse(xPoints, Brushes.Black, 3, 3));
            _children.Add(DrawHelpers.DrawEllipse(yPoints, Brushes.Black, 3, 3));

            double scaleStep = xInterval;
            if (xMax == 1)
            {
                scaleStep = xMax / xInterval;
            }
            // Draw the figures on the axises
            _children.Add(DrawScaleText(xPoints, scaleStep, 10, Brushes.Black));

            scaleStep = yInterval;
            if (yMax == 1)
            {
                scaleStep = yMax / yInterval;
            }
            _children.Add(DrawScaleText(yPoints, scaleStep, 10, Brushes.Black, false));
        }
        private static double CalculateActualSize(double source, int offset)
        {
            return source - offset * 2;
        }
        // Helper function to calculate step in scale
        private static double CalculateStepValue(double numberOfPoints, double size)
        {
            return size / numberOfPoints;
        }
        // Helper function to calculate point collection for x and y
        private static PointCollection CalculatePointsForScale(double numberOfPoints, double stepValue, int offset, double startY, bool x = true)
        {
            // TODO startX and startY?
            PointCollection points = new PointCollection
            {
                // Add origo point            
                new Point(offset, startY)
            };
            for (int i = 1; i < numberOfPoints + 1; i++)
            {
                if (x == true)
                {
                    // For X we increase by stepvalue + offset, but keep Y at the same coordinate
                    points.Add(new Point(i * stepValue + offset, startY));
                }
                else
                {
                    // For Y we keep X at offset
                    points.Add(new Point(offset, startY - (i * stepValue)));
                }
            }
            return points;
        }
        // Helper method to draw scale text
        private DrawingVisual DrawScaleText(PointCollection points, double interval, int size, Brush color, bool x = true)
        {

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            FlowDirection direction = FlowDirection.RightToLeft;
            if (x == true)
            {
                direction = FlowDirection.LeftToRight;
            }
            
            for (int i = 0; i < points.Count; i++) { 
                string scaleText = Utilities.Utilities.FormatDecimalString(interval * i);
                DrawHelpers.DrawText(ref context, scaleText, points[i], direction, size, color);
            }
            context.Close();
            return visual;
        }
    }
}
