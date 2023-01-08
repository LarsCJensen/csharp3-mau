using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static LoveYourBudget.Diagram.Enums;

namespace LoveYourBudget.Diagram
{
    public static class Calculator
    {
        // Helper method to calculate canvas size
        public static double CalculateActualSize(double source, int offset)
        {
            return source - offset;
        }
        // Helper function to calculate step in scale
        public static double CalculateStepValue(double numberOfPoints, double size)
        {
            return size / numberOfPoints;
        }
        // Helper function to calculate step in scale
        public static double CalculateScale(double size, double maxValue)
        {
            return size / maxValue;
        }
        // Helper function to calculate point collection for x and y
        public static PointCollection GetPointsForScale(double numberOfSteps, double stepValue, int offset, double startY, Orientation orientation)
        {
            // TODO Is this needed?
            //PointCollection points = new PointCollection
            //{
            //    // Add origo point            
            //    new Point(offset, startY)
            //};
            PointCollection points = new PointCollection();
            // TODO REMOVE
            // Since we add an origo point we skip one of the steps passed in
            for (int i = 0; i < numberOfSteps; i++)
            {
                if (orientation == Orientation.Horizontal)
                {
                    // For horizontal we increase by stepvalue + offset, but keep Y at the same coordinate
                    points.Add(new Point(i * stepValue + offset, startY));
                }
                else if (orientation == Orientation.Vertical)
                {
                    // For vertical we keep X at offset
                    points.Add(new Point(offset, startY - (i * stepValue)));
                }
            }
            return points;
        }
        // Transforms points to canvas units
        public async static Task<PointCollection> TransformPointsToCanvas(PointCollection points,double canvasHeight, double xCanvasScale, double yCanvasScale, int offset)
        {
            PointCollection transformedPoints = new PointCollection();
            foreach (Point point in points)
            {
                transformedPoints.Add(TransformPointToCanvas(point, canvasHeight, xCanvasScale, yCanvasScale, offset));
            }
            return transformedPoints;
        }
        // Transfor point to canvas units
        public static Point TransformPointToCanvas(Point point, double canvasHeight, double xCanvasScale, double yCanvasScale, int offset)
        {
            double x = (point.X * xCanvasScale) + offset;
            double y = canvasHeight - ((point.Y * yCanvasScale) + offset);
            return new Point(x, y);
        }
    }
}
