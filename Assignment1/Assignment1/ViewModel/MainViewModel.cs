using Assignment1_Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.IO;
using Assignment1_BLL;

namespace Assignment1.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        // TODO Is this needed?
        private ObservableCollection<FileClass> _files = new ObservableCollection<FileClass>();
        public ObservableCollection<FileClass> Files
        {
            get { return _files; }
            private set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }

        public RelayCommand<object> SelectFolder { get; private set; }
        public MainViewModel()
        {
            SelectFolder = new RelayCommand<object>(param => SelectFolderExcecute(param));
        }

        private void SelectFolderExcecute(object sender)
        {
            // TODO Try catch!
            TreeViewItem selecteditem = (TreeViewItem)sender;
            string[] extensions = new string[] { "*.jpg", "*.bmp", "*.mov", "*.avi", "*.mpg", "*.mp4" };
            
            List<FileInfo> filesFileInfo = FileUtilities.GetFileInfoFromDirectory(selecteditem.Tag.ToString(), extensions);
            Files = new ObservableCollection<FileClass>();
            foreach(FileInfo file in filesFileInfo)
            {
                FileClass newFile = new FileClass(file);
                Files.Add(newFile);
            }
        }
    }
}
