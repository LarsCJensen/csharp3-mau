﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Utilities;
using static LoveYourBudget.Diagram.Enums;

namespace LoveYourBudget.Diagram
{
    public class VisualHost: FrameworkElement
    {
        private VisualCollection _children;
        int _offset = 40;        
        double _canvasHeight;        

        //  TODO REMOVE? Local variables to hold the scale when transforming points
        double _xCanvasScale;
        double _yCanvasScale;
        
        
        // TODO REMOVE
        //double _xPointScale;
        //double _yPointScale;

        // TODO REMOVE
        private double _xActualSize;
        private double _yActualSize;

        private double _actualSize;

        public VisualHost(double height) 
        {
            _canvasHeight = height;
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
        //public void DrawScale(double xMax, int xInterval, double xWidth, double startX, double yMax, int yInterval, double yHeight, double startY)
        //{
        //    // TODO
        //    //_yHeight = yHeight;
        //    // Actual size of the diagram area
        //    _xActualSize = CalculateActualSize(xWidth, _offset);
        //    _yActualSize = CalculateActualSize(yHeight, _offset);

        //    // Coordinates used to convert to canvas units
        //    _xCanvasScale = _xActualSize / xMax;
        //    _yCanvasScale = _yActualSize / yMax;

        //    // Coordinates used to convert from canvas units
        //    _xPointScale = xMax / _xActualSize;
        //    _yPointScale = yMax / _yActualSize;

        //    // Value of each step in the diagram
        //    // Ugly workaround in case someone uses max value of 1
        //    double numberOfPoints;
        //    if (xMax == 1)
        //    {
        //        numberOfPoints = xInterval;
        //    }
        //    else
        //    {
        //        numberOfPoints = xMax / xInterval;
        //    }

        //    double _stepValueX = CalculateStepValue(Math.Round(numberOfPoints), _xActualSize);
        //    PointCollection xPoints = CalculatePointsForScale(numberOfPoints, _stepValueX, _offset, yHeight - _offset);

        //    if (yMax == 1)
        //    {
        //        numberOfPoints = yInterval;
        //    }
        //    else
        //    {
        //        numberOfPoints = yMax / yInterval;
        //    }

        //    double _stepValueY = CalculateStepValue(Math.Round(numberOfPoints), _yActualSize);
        //    PointCollection yPoints = CalculatePointsForScale(numberOfPoints, _stepValueY, _offset, yHeight - _offset, false);

        //    _children.Add(DrawHelpers.DrawLine(xPoints, Brushes.Black, 2));
        //    _children.Add(DrawHelpers.DrawLine(yPoints, Brushes.Black, 2));

        //    // Draw scale markers using Ellips            
        //    _children.Add(DrawHelpers.DrawEllipse(xPoints, Brushes.Black, 3, 3));
        //    _children.Add(DrawHelpers.DrawEllipse(yPoints, Brushes.Black, 3, 3));

        //    double scaleStep = xInterval;
        //    if (xMax == 1)
        //    {
        //        scaleStep = xMax / xInterval;
        //    }
        //    // Draw the figures on the axises
        //    _children.Add(DrawScaleText(xPoints, scaleStep, 10, Brushes.Black));

        //    scaleStep = yInterval;
        //    if (yMax == 1)
        //    {
        //        scaleStep = yMax / yInterval;
        //    }
        //    _children.Add(DrawScaleText(yPoints, scaleStep, 10, Brushes.Black, false));
        //}
        public async Task DrawScaleAxis(List<string> axisLabels, double max, double size, Orientation orientation)
        {
            // Actual size of the diagram area
            _actualSize = Calculator.CalculateActualSize(size, _offset);

            // TODO NUMBER OF LABELS IS 11 NOT 10 AS IN Ass6
            // Value of each step in the diagram
            double numberOfPoints = axisLabels.Count;
            double stepValue = Calculator.CalculateStepValue(numberOfPoints, _actualSize);
            // Coordinates used to convert from canvas units
            if (orientation == Orientation.Horizontal)
            {
                //_xPointScale = max / _actualSize;
                // Coordinates used to convert to canvas units
                _xCanvasScale = Calculator.CalculateScale(_actualSize, max);
            } else
            {
                //_yPointScale = max / _actualSize;
                // Coordinates used to convert to canvas units
                _yCanvasScale = Calculator.CalculateScale(_actualSize, max);
                // TODO Ugly way to handle this
                stepValue = Calculator.CalculateStepValue(numberOfPoints-1, _actualSize);
            }           
                       
            
            PointCollection points = Calculator.GetPointsForScale(numberOfPoints, stepValue, _offset, _canvasHeight - _offset, orientation);
            
            // TODO REMOVE COMMENTED CODE
            //if (yMax == 1)
            //{
            //    numberOfPoints = yInterval;
            //}
            //else
            //{
            //    numberOfPoints = yMax / yInterval;
            //}

            //double _stepValueY = CalculateStepValue(Math.Round(numberOfPoints), _yActualSize);
            //PointCollection yPoints = CalculatePointsForScale(numberOfPoints, _stepValueY, _offset, yHeight - _offset, false);

            _children.Add(DrawHelpers.DrawLine(points, Brushes.Black, 2));
            //_children.Add(DrawHelpers.DrawLine(yPoints, Brushes.Black, 2));

            // Draw scale markers using Ellips            
            _children.Add(DrawHelpers.DrawEllipse(points, Brushes.Black, 3, 3));
            //_children.Add(DrawHelpers.DrawEllipse(yPoints, Brushes.Black, 3, 3));

            //double scaleStep = xInterval;
            //if (xMax == 1)
            //{
            //    scaleStep = xMax / xInterval;
            //}
            
            // Draw the figures on the axises
            _children.Add(DrawScaleText(points, axisLabels, 10, Brushes.Black, orientation));

            //scaleStep = yInterval;
            //if (yMax == 1)
            //{
            //    scaleStep = yMax / yInterval;
            //}
            //_children.Add(DrawScaleText(yPoints, scaleStep, 10, Brushes.Black, false));
        }
        public async Task DrawLegend(Dictionary<string, Brush> labels)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            FlowDirection direction = FlowDirection.RightToLeft;
            double startX = _offset;
            double startY = _canvasHeight - 20;
            foreach (var label in labels)
            {
                PointCollection legendLine = new PointCollection()
                {
                    new Point(startX, startY),
                    new Point(startX + 20, startY),
                };
                Point textPoint = new Point(startX, startY);
                _children.Add(DrawHelpers.DrawLine(legendLine, label.Value,2,false));                
                _children.Add(DrawText(textPoint, label.Key, 10, label.Value, Orientation.Horizontal));
                startX += 50;
                //DrawHelpers.DrawText(ref context, label.Key, points[0], direction, 15, label.Value);
            }
            
        }
        //// Helper method to calculate canvas size
        //private static double CalculateActualSize(double source, int offset)
        //{
        //    return source - offset;
        //}
        //// Helper function to calculate step in scale
        //private static double CalculateStepValue(double numberOfPoints, double size)
        //{
        //    return size / numberOfPoints;
        //}
        //// Helper function to calculate point collection for x and y
        //private static PointCollection CalculatePointsForScale(double numberOfSteps, double stepValue, int offset, double startY, Orientation orientation)
        //{
        //    PointCollection points = new PointCollection
        //    {
        //        // Add origo point            
        //        new Point(offset, startY)
        //    };
        //    // Since we add an origo point we skip one of the steps passed in
        //    for (int i = 1; i < numberOfSteps; i++)
        //    {
        //        if (orientation == Orientation.Horizontal)
        //        {
        //            // For horizontal we increase by stepvalue + offset, but keep Y at the same coordinate
        //            points.Add(new Point(i * stepValue + offset, startY));
        //        }
        //        else if (orientation == Orientation.Vertical)
        //        {
        //            // For vertical we keep X at offset
        //            points.Add(new Point(offset, startY - (i * stepValue)));
        //        }
        //    }
        //    return points;
        //}
        // Helper method to draw scale text
        private DrawingVisual DrawScaleText(PointCollection points, List<string> labels, int size, Brush color, Orientation orientation)
        {

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            FlowDirection direction = FlowDirection.RightToLeft;
            if (orientation == Orientation.Horizontal)
            {
                direction = FlowDirection.LeftToRight;
            }
            
            for (int i = 0; i < points.Count; i++) {                 
                DrawHelpers.DrawText(ref context, labels[i], points[i], direction, size, color);
            }
            context.Close();
            return visual;
        }
        // Helper method to draw scale text
        private DrawingVisual DrawText(Point point, string label, int size, Brush color, Orientation orientation)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            FlowDirection direction = FlowDirection.RightToLeft;
            if (orientation == Orientation.Horizontal)
            {
                direction = FlowDirection.LeftToRight;
            }
            DrawHelpers.DrawText(ref context, label, point, direction, size, color);
            context.Close();
            return visual;
        }
        /// <summary>
        /// Draw points based on collection. Will transform points to canvas values
        /// </summary>
        /// <param name="points"></param>
        public async Task DrawPointsAsync(PointCollection points, Brush color, int size=1)
        {
            PointCollection calculatedPoints = await Calculator.TransformPointsToCanvas(points, _canvasHeight, _xCanvasScale, _yCanvasScale, _offset);
            _children.Add(DrawHelpers.DrawLine(calculatedPoints, color, size));
        }
        /// <summary>
        /// Draw points based on collection. Will transform points to canvas values
        /// </summary>
        /// <param name="points">points to draw</param>
        /// <param name="color">color</param>
        /// <param name="size">size</param>
        public async Task DrawStackAsync(PointCollection points, Brush color, int size = 1)
        {
            PointCollection calculatedPoints = await Calculator.TransformPointsToCanvas(points, _canvasHeight, _xCanvasScale, _yCanvasScale, _offset);
            _children.Add(DrawHelpers.DrawStacks(calculatedPoints, _canvasHeight - _offset, color, size));
        }
    }
}