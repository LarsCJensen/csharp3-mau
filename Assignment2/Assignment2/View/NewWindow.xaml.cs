using Assignment2.ViewModel;
using Assignment2.Utilities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using Assignment2.BLL;
using Assignment2.BLL.Model;

namespace Assignment2.View
{
    /// <summary>
    /// Interaction logic for NewWindow.xaml
    /// </summary>
    public partial class NewWindow : Window
    {
        private object? dummyNode = null;
        public NewWindow()
        {
            InitializeComponent();
            InitializeGUI();            
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
                    Dialogs.DialogService.DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("Exception occurred!", ex.Message);
                    Dialogs.DialogService.DialogService.OpenDialog(errorVM);
                }
            }
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

            NewWindowViewModel vm = this.DataContext as NewWindowViewModel;
            vm.AddCommand.Execute(filesListBox.SelectedItem);
        }
    }
}
