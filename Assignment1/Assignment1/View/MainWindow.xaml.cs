using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Assignment1.View;
using Assignment1.ViewModel;
using Assignment1_BLL;
using Assignment1_Utilities;

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
                    MessageBox.Show(ex.Message, "Exception occurred!", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Would log callstack
                }
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            PlayerViewModel playerVm = new PlayerViewModel(vm.Slideshow.Name, vm.Slideshow.Files);
            Player player= new Player();
            // Bind OnClose event
            playerVm.OnClose += delegate { this.Close(); };
            player.DataContext = playerVm;
            player.Show();
        }
    }
}
