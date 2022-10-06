using Assignment2.DAL.Models;
using Assignment2.ViewModel;
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

namespace Assignment2.View
{
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        ListViewModel vm = new ListViewModel();
        public ListWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
            vm.OnClose += delegate { this.Close(); };
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            // TODO Dialog
            if (vm.SelectedSlideshow == null)
            {
                MessageBox.Show("No slideshow selected!", "Please select slideshow", MessageBoxButton.OK, MessageBoxImage.Error);

            } else
            {
                PlayerViewModel playerVm = new PlayerViewModel(vm.SelectedSlideshow.Title, vm.SelectedSlideshow.Files, vm.SelectedSlideshow.Interval);
                Player player = new Player();
                // Bind OnClose event
                playerVm.OnClose += delegate { this.Close(); };
                player.DataContext = playerVm;
                player.Show();
            }
        }

        private void NewAlbum_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(false);
        }
        private void NewSlideshow_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(true);
        }
        private void OpenNewWindow(bool slideshow)
        {
            NewWindowViewModel newVm = new NewWindowViewModel(slideshow);
            NewWindow newWindow = new NewWindow();

            // Bind OnClose event
            newWindow.DataContext = newVm;
            newVm.OnClose += delegate { newWindow.Close(); };
            newVm.OnSave += vm.OnSave;
            newWindow.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            NewWindow newWindow = new NewWindow();
            NewWindowViewModel newVm = null;
            if (vm.SelectedAlbum != null)
            {
                newVm = new NewWindowViewModel(vm.SelectedAlbum.id, false);
                
            } else if (vm.SelectedSlideshow != null)
            {
                newVm = new NewWindowViewModel(vm.SelectedSlideshow.id, true);                
            }

            // Bind OnClose event
            newWindow.DataContext = newVm;
            newVm.OnClose += delegate { newWindow.Close(); };
            newVm.OnSave += vm.OnSave;
            newWindow.Show();

        }
    }
}
