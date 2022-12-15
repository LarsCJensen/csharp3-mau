using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            _visualHost.DrawStuff();
            // TODO Clears the children, which allows other children to be added
            diagramCanvas.Children.Clear();
            diagramCanvas.Children.Add(_visualHost);
        }

        private void Save_Settings_Click(object sender, RoutedEventArgs e)
        {
            // TODO If values are not equally divided, inform that
            //int xmin = 0;
            int xMax = 1000;
            int xInterval = 100;
            // Leave pixels for space
            double xWidth = diagramCanvas.ActualWidth;
            //int ymin = 0;
            int yMax = 1000;
            int yInterval = 100;
            double yHeight = diagramCanvas.ActualHeight;
            // Calculate scale
            _visualHost.DrawScale(xMax, xInterval, xWidth, yMax, yInterval, yHeight);
            // TODO Don't add later.
            diagramCanvas.Children.Add(_visualHost);

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Coordinates.Items.Add(new Point(200,400));
            Coordinates.Items.Add(new Point(100, 500));
            // Redraw
        }
    }
}
