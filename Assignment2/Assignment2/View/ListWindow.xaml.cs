using Assignment2.DAL.Models;
using Assignment2.Dialogs.DialogService;
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
        /// <summary>
        /// Event for Play slideshow. I find this is ok to have in code-behind 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedSlideshow == null)
            {
                DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("Please select slideshow", "No slideshow selected!");
                DialogService.OpenDialog(errorVM);
                
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
        /// <summary>
        /// Event for new album button click. I think it is ok to have this in code-behind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewAlbum_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(false);
        }
        /// <summary>
        /// Event for new slideshow button click. I think it is ok to have this in code-behind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewSlideshow_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(true);
        }
        /// <summary>
        /// Helper to open window with correct type
        /// </summary>
        /// <param name="slideshow">Set is slideshow</param>
        private void OpenNewWindow(bool slideshow)
        {
            NewWindowViewModel newVm = new NewWindowViewModel(slideshow);
            NewWindow newWindow = new NewWindow();

            newWindow.DataContext = newVm;
            // Bind OnClose event
            newVm.OnClose += delegate { newWindow.Close(); };
            // Bind OnSave event
            newVm.OnSave += vm.OnSave;
            newWindow.Show();
        }
        /// <summary>
        /// Event for edit button click. I think it is ok to have this in code-behind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // A bit ugly, but works
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
            // Bind OnSave event
            newVm.OnSave += vm.OnSave;
            newWindow.Show();
        }
    }
}
