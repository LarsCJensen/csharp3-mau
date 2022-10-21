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
            // Lambda Expression 1
            bugViewModel.OnClose += delegate { bugView.Close(); };
            // Bind to event OnSave from bug window
            bugViewModel.OnSave += vm.OnSave;
            bugView.Show();
        }

        private void Developers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open Developers view");
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if(vm.SelectedBug != null)
            {
                BugViewModel bugViewModel = new BugViewModel(vm.SelectedBug);
                BugView bugView = new BugView();
                bugView.DataContext = bugViewModel;
                // Lambda Expression 1
                bugViewModel.OnClose += delegate { bugView.Close(); };
                // Bind to event OnSave from bug window
                bugViewModel.OnSave += vm.OnSave;
                bugView.Show();
            } else
            {
                MessageBox.Show("Please choose bug to open!", "Choose bug!");
            }
            
        }
        private void ListViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Open_Click(sender, e);
        }        
    }
}
