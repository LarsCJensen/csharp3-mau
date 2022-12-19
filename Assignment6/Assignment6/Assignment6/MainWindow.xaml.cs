using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        List<Point> _points = new List<Point>();
        VisualHost _visualHost = new VisualHost();
        // Local variables for settings
        int _xMax = 0;
        int _xInterval = 0;
        double _xWidth = 0;
        int _yMax = 0;
        int _yInterval = 0;
        double _yHeight = 0;
        string _diagramTitle;
        CollectionView _view;

        public MainWindow()
        {
            InitializeComponent();
            groupBoxCoordinates.IsEnabled = false;
            Coordinates.ItemsSource = _points;
            _view = (CollectionView)CollectionViewSource.GetDefaultView(Coordinates.ItemsSource);
        }
        
        // Helper method to clear diagram
        private void Clear_Diagram_Click(object sender, RoutedEventArgs e)
        {            
            // Clears the children, which allows other children to be added
            diagramCanvas.Children.Clear();
            groupSettings.IsEnabled = true;
            groupBoxCoordinates.IsEnabled = false;
            Coordinates.ItemsSource = null;
            _points = new List<Point>();
        }
        // Event for save settings
        private void Save_Settings_Click(object sender, RoutedEventArgs e)
        {
            Coordinates.ItemsSource = _points;
            _xMax = TryParseToInt(XMax.Text);
            _xInterval = TryParseToInt(XInterval.Text);
            _xWidth = diagramCanvas.ActualWidth;
            _yMax = TryParseToInt(YMax.Text);
            _yInterval = TryParseToInt(YInterval.Text);
            _yHeight = diagramCanvas.ActualHeight;
            _diagramTitle = DiagramTitle.Text;
            _visualHost = new VisualHost();
            if (NoZeroValues() && _diagramTitle is not "")
            {
                groupSettings.IsEnabled = false;
                groupBoxCoordinates.IsEnabled = true;
                _visualHost.DrawScale(_xMax, _xInterval, _xWidth, _yMax, _yInterval, _yHeight, _diagramTitle);
                diagramCanvas.Children.Add(_visualHost);
            } else
            {
                MessageBox.Show("Title required and zero values is not allowed!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Event for Add click
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _points.Add(new Point(TryParseToInt(XCoordinate.Text), TryParseToInt(YCoordinate.Text)));
            Coordinates.ItemsSource = _points;
            // Clear filter
            _view.SortDescriptions.Clear();
            RefreshView();
            XCoordinate.Text = string.Empty;
            YCoordinate.Text = string.Empty;
        }
        // Event for Draw click
        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            diagramCanvas.Children.Clear();
            _visualHost = new VisualHost();
            Draw();
        }
        // Event for Window size changed
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Reset visualHost
            diagramCanvas.Children.Clear();
            _visualHost = new VisualHost();
            _xWidth = diagramCanvas.ActualWidth;
            _yHeight = diagramCanvas.ActualHeight;
            // This check is needed since SizeChanged is called before settings are set
            if (NoZeroValues())
            {
                Draw();
            }
        }
        // Helper function to draw scale and points
        private void Draw()
        {
            _visualHost.DrawScale(_xMax, _xInterval, _xWidth, _yMax, _yInterval, _yHeight, _diagramTitle);
            PointCollection points = new PointCollection();
            foreach(Point point in Coordinates.Items)
            {
                points.Add(point);
            }
            _visualHost.DrawPoints(points);
            // Add the visual host to the canvas
            diagramCanvas.Children.Add(_visualHost);
            
        }
        // Helper function to make sure we don't get zero values
        private bool NoZeroValues()
        {
            if (_xMax == 0 || _xInterval == 0 || _xWidth == 0 || _yMax == 0 || _yInterval == 0 || _yHeight == 0)
                return false;
            return true;
        }
        // Helper to try to parse text to int
        private int TryParseToInt(string text)
        {
            if(int.TryParse(text, out int i))
            {
                return i;
            } else
            {
                //MessageBox.Show("Could not parse text to int", "Parse error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }
        // Function to Exit program
        private void Close_Executed(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // Simple event to sort by X/Y
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            string menuHeader = menuItem.Header as string;
            _view.SortDescriptions.Clear();
            if (menuHeader == "_Sort_by_X")
            {
                // Sort listview by X
                _view.SortDescriptions.Add(new SortDescription("X", ListSortDirection.Ascending));
            }
            else
            {
                // Sort listview by Y
                _view.SortDescriptions.Add(new SortDescription("Y", ListSortDirection.Ascending));
            }
        }
        // Event to catch left mouse button
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!NoZeroValues())
                return;
            // Transform point before adding it
            _view.SortDescriptions.Clear();
            Point newPoint = e.GetPosition((UIElement)sender);
            Point transFormedPoint = _visualHost.TransformCanvasToPoint(newPoint);
            _points.Add(transFormedPoint);
            Coordinates.Items.Refresh();
            Draw_Click(sender, e);
        }
        // Helper function to refresh view
        private void RefreshView()
        {            
            _view = (CollectionView)CollectionViewSource.GetDefaultView(Coordinates.ItemsSource);
            Coordinates.Items.Refresh();
        }
    }
}
