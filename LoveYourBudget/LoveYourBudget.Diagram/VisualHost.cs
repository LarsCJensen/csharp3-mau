using System;
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
    /// <summary>
    /// Class used to draw
    /// </summary>
    public class VisualHost: FrameworkElement
    {
        private VisualCollection _children;
        int _offset = 40;        
        double _canvasHeight;        
       
        double _xCanvasScale;
        double _yCanvasScale;        
        
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
        
        public void DrawScaleAxis(List<string> axisLabels, double max, double size, Orientation orientation)
        {            
            // Actual size of the diagram area
            _actualSize = Calculator.CalculateActualSize(size, _offset);

            // Value of each step in the diagram
            double numberOfPoints = axisLabels.Count;
            double stepValue = Calculator.CalculateStepValue(numberOfPoints, _actualSize);
            // Coordinates used to convert from canvas units
            if (orientation == Orientation.Horizontal)
            {
                // Coordinates used to convert to canvas units
                _xCanvasScale = Calculator.CalculateScale(_actualSize, max);
            } else
            {
                // Coordinates used to convert to canvas units
                _yCanvasScale = Calculator.CalculateScale(_actualSize, max);
                // FUTURE Ugly way to handle this
                // To handle Y axis with label for 0 I need to reduce number of points with that
                stepValue = Calculator.CalculateStepValue(numberOfPoints-1, _actualSize);
            }                                  
            
            PointCollection points = Calculator.GetPointsForScale(numberOfPoints, stepValue, _offset, _canvasHeight - _offset, orientation);
            
            _children.Add(DrawHelpers.DrawLine(points, Brushes.Black, 2));
            
            // Draw scale markers using Ellips            
            _children.Add(DrawHelpers.DrawEllipse(points, Brushes.Black, 3, 3));
            
            // Draw the figures on the axises
            _children.Add(DrawScaleText(points, axisLabels, 10, Brushes.Black, orientation));
        }
        /// <summary>
        /// Method to Draw legend
        /// </summary>
        /// <param name="labels">labels to draw</param>
        /// <returns></returns>
        public async Task AsyncDrawLegend(Dictionary<string, Brush> labels)
        {
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
            }
            
        }
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
        public void DrawPoints(PointCollection points, Brush color, int size=1)
        {
            PointCollection calculatedPoints = Calculator.TransformPointsToCanvas(points, _canvasHeight, _xCanvasScale, _yCanvasScale, _offset);
            _children.Add(DrawHelpers.DrawLine(calculatedPoints, color, size));
        }
        /// <summary>
        /// Draw points based on collection. Will transform points to canvas values
        /// </summary>
        /// <param name="points">points to draw</param>
        /// <param name="color">color</param>
        /// <param name="size">size</param>
        public void DrawStack(PointCollection points, Brush color, int size = 1)
        {
            PointCollection calculatedPoints = Calculator.TransformPointsToCanvas(points, _canvasHeight, _xCanvasScale, _yCanvasScale, _offset);
            _children.Add(DrawHelpers.DrawStacks(calculatedPoints, _canvasHeight - _offset, color, size));
        }
    }
}
