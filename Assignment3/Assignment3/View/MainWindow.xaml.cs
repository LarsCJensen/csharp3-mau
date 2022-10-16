using Assignment3.ViewModel;
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

namespace Assignment3.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel vm = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
            vm.OnClose += delegate { this.Close(); };
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            BugViewModel bugViewModel = new BugViewModel();
            BugView bugView = new BugView();
            bugView.DataContext = bugViewModel;
            bugViewModel.OnClose += delegate { bugView.Close(); };
            // Bind to event OnSave from bug window
            bugViewModel.OnSave += vm.OnSave;
            bugView.Show();
        }
    }
}
