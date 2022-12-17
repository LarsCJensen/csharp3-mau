using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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

namespace Assignment6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VisualHost _visualHost = new VisualHost();
        int _xMax = 0;
        int _xInterval = 0;
        double _xWidth = 0;
        int _yMax = 0;
        int _yInterval = 0;
        double _yHeight = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Clear_Diagram_Click(object sender, RoutedEventArgs e)
        {            
            // Clears the children, which allows other children to be added
            diagramCanvas.Children.Clear();
            groupSettings.IsEnabled = true;
        }

        private void Save_Settings_Click(object sender, RoutedEventArgs e)
        {
            _xMax = TryParseToInt(XMax.Text);
            _xInterval = TryParseToInt(XInterval.Text);
            _xWidth = diagramCanvas.ActualWidth;
            _yMax = TryParseToInt(YMax.Text);
            _yInterval = TryParseToInt(YInterval.Text);
            _yHeight = diagramCanvas.ActualHeight;
            if (NoZeroValues())
            {
                groupSettings.IsEnabled = false;
            } else
            {
                MessageBox.Show("Zero values is not allowed!", "No zero values!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Coordinates.Items.Add(new Point(TryParseToInt(XCoordinate.Text), TryParseToInt(YCoordinate.Text)));
            XCoordinate.Text = string.Empty;
            YCoordinate.Text = string.Empty;
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            diagramCanvas.Children.Clear();
            _visualHost = new VisualHost();
            Draw();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Reset visualHost
            diagramCanvas.Children.Clear();
            _visualHost = new VisualHost();
            _xWidth = diagramCanvas.ActualWidth;
            _yHeight = diagramCanvas.ActualHeight;
            Draw();            
        }
        private void Draw()
        {
            if(NoZeroValues())
            {
                _visualHost.DrawScale(_xMax, _xInterval, _xWidth, _yMax, _yInterval, _yHeight);
                PointCollection points = new PointCollection();
                foreach(Point point in Coordinates.Items)
                {
                    points.Add(point);
                }
                _visualHost.DrawPoints(points);
                diagramCanvas.Children.Add(_visualHost);
            }            
        }
        private bool NoZeroValues()
        {
            if (_xMax == 0 || _xInterval == 0 || _xWidth == 0 || _yMax == 0 || _yInterval == 0 || _yHeight == 0)
                return false;
            return true;
        }
        private int TryParseToInt(string text)
        {
            if(int.TryParse(text, out int i))
            {
                return i;
            } else
            {
                MessageBox.Show("Could not parse text to int", "Parse error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }
    }
}
