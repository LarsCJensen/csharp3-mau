using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace LoveYourBudget.Diagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DiagramWindow : Window
    {
        VisualHost _visualHost = new VisualHost();
        string _test;
        public DiagramWindow(string test)
        {
            InitializeComponent();            
            _test = test;
        }
        // TODO Async
        private async void DrawYearlyOverView()
        {
            double xMax = 12;
            int xInterval = 1;
            double startX = 1;
            double yMax = 10000;
            int yInterval = 1000;
            double startY = 0;

            await DrawScale(xMax, xInterval, diagramCanvas.ActualWidth, startX, yMax, yInterval, diagramCanvas.ActualHeight, startY);
        }
        private async Task DrawScale(double xMax, int xInterval, double xWidth, double startX, double yMax, int yInterval, double yHeight, double startY)
        {
            _visualHost.DrawScale(xMax, xInterval, xWidth, startX, yMax, yInterval, yHeight, startY);

            diagramCanvas.Children.Add(_visualHost);
        }
        // Event for Window size changed
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // TODO
            //// Reset visualHost
            //diagramCanvas.Children.Clear();
            //_visualHost = new VisualHost();
            //_xWidth = diagramCanvas.ActualWidth;
            //_yHeight = diagramCanvas.ActualHeight;
            //// This check is needed since SizeChanged is called before settings are set
            //if (NoZeroValues())
            //{
            //    Draw();
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_test == "YearlyOverView")
            {
                DrawYearlyOverView();
            }
        }
    }
}
