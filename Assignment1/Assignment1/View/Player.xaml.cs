using Assignment1.ViewModel;
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
using System.Windows.Shapes;

namespace Assignment1.View
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : Window
    {

        PlayerViewModel vm = new PlayerViewModel();
        public Player()
        {
            InitializeComponent();            
            DataContext = vm;         
            vm.OnClose += delegate { this.Close(); };
        }
    }
}
