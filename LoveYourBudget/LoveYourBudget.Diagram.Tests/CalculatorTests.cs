using LoveYourBudget.Diagram;
using System;
using System.Windows;

namespace LoveYourBudget.Diagram.Tests
{
    /// <summary>
    /// Tests for Calculator
    /// </summary>
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestCalculateActualSize()
        {
            // Arrange
            var source = 100;
            var offset = 20;
            // Act
            var result = Calculator.CalculateActualSize(source, offset);
            // Assert            
            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public void TestCalculateStepValue()
        {
            // Arrange
            var numberOfSteps = 10;
            var size = 100;
            // Act
            var result = Calculator.CalculateStepValue(numberOfSteps, size);
            // Assert            
            Assert.AreEqual(10, result);
        }
        [TestMethod]
        public void TestTransformPointToCanvas()
        {
            // Arrange
            var point = new Point(100, 100);
            var canvasHeight = 1000;
            var xCanvasScale = 1;
            var yCanvasScale = 1;
            var offset = 0;
            // Act
            var result = Calculator.TransformPointToCanvas(point, canvasHeight, xCanvasScale, yCanvasScale, offset);
            // Assert            
            Assert.AreEqual(new Point(100, 900), result);
        }
        [TestMethod]
        public void TestTransformPointToCanvasWithOffset()
        {
            // Arrange
            var point = new Point(100, 100);
            var canvasHeight = 1000;
            var xCanvasScale = 1;
            var yCanvasScale = 1;
            var offset = 20;
            // Act
            var result = Calculator.TransformPointToCanvas(point, canvasHeight, xCanvasScale, yCanvasScale, offset);
            // Assert            
            Assert.AreEqual(new Point(120, 880), result);
        }
        [TestMethod]
        public void TestCalculateScale()
        {
            // Arrange            
            var actualSize = 1000;
            var max = 10000;
            // Act
            var result = Calculator.CalculateScale(actualSize, max);
            // Assert            
            Assert.AreEqual(0.1, result);
        }
    }
}