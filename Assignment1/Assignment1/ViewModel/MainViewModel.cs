using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment1.ViewModel
{
    public class MainViewModel
    {

        public RelayCommand LoadTestData { get; private set; }

        public MainViewModel()
        {
            LoadTestData = new RelayCommand(LoadTestDataExecute);
        }

        private void LoadTestDataExecute(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MessageBox.Show("Test");
        }
    }
}
