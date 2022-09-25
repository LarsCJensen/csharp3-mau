using Assignment1.ViewModel;
using Assignment1_Utilities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Assignment1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object? dummyNode = null;
        MainViewModel vm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();            
            DataContext = vm;            
            InitializeGUI();
            vm.OnClose += delegate { this.Close(); };

        }
        private void InitializeGUI()
        {
            string[] drives = FileUtilities.AddLogicalDrives();
            foreach (string drive in drives)
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = drive;
                item.Tag = drive;
                item.FontWeight = FontWeights.Normal;
                // Adding a dummyNode to make first expand work
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(FolderExpanded);
                fileTreeView.Items.Add(item);
            }           
        }
        /// <summary>
        /// Event for when folder is expanded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            // If top node is expanded
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    // Add subfolders to list
                    string[] directories = FileUtilities.GetDirectories(item.Tag.ToString());
                    foreach (string s in directories)
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(FolderExpanded);
                        item.Items.Add(subitem);                        
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Exception occurred!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Dialogs.DialogService.DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("Exception occurred!", ex.Message);
                    Dialogs.DialogService.DialogService.OpenDialog(errorVM);
                }
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            PlayerViewModel playerVm = new PlayerViewModel(vm.Slideshow.Title, vm.Slideshow.Files, vm.Slideshow.Interval);
            Player player= new Player();
            // Bind OnClose event
            playerVm.OnClose += delegate { this.Close(); };
            player.DataContext = playerVm;
            player.Show();
        }

        // This will be moved to an about dialog
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string aboutMessage = "To create a new album use menu File -> New Album.";
            aboutMessage += "\nTo create a new slideshow use menu File -> New slideshow.";
            aboutMessage += $"\n\nBrowse for files through the tree view. Supported file types are {String.Join(",", ValidExtensions.AllValidExtensions)}.";
            aboutMessage += "\n\nFor slideshows you can choose interval to be used between images. Videos will be played in its full length.";
            MessageBox.Show(aboutMessage, "How to?", MessageBoxButton.OK);
        }
        /// <summary>
        /// Event for double click on image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Wanted to make this a command, but couldn't find a good solution.
        /// Sometimes code-behind isn't that bad.
        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            vm.AddCommand.Execute(filesListBox.SelectedItem);
        }
    }
}
