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
using CommunityToolkit.Mvvm.ComponentModel;

namespace Assignment1.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        private string _title = "Home Media Player";
        public string Title
        {
            get { return _title; }
            set { 
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        private bool _isInitialized = false;
        public bool IsInitialized { 
            get
            {
                return _isInitialized;
            }
            set
            {
                _isInitialized = value;
                OnPropertyChanged("IsInitialized");
            } 
        }
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
        private object _createdObject = null;
        public object CreatedObject { 
            get
            {
                return _createdObject;
            }
            set
            {
                _createdObject = value;
                OnPropertyChanged("CreatedObject");
            } 
        }

        #region Commands
        public RelayCommand<object> SelectFolder { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand<string> NewCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        #endregion
        #region EventHandlers
        public event EventHandler OnClose;
        #endregion
        public MainViewModel()
        {
            SelectFolder = new RelayCommand<object>(param => SelectFolderExcecute(param));
            CloseCommand = new RelayCommand(Close);
            NewCommand = new RelayCommand<string>(param => NewCommandExecute(param));
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
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
        private void NewCommandExecute(string commandParam)
        {
            // Change contents of gui?
            CreatedObject = new AlbumClass();
            IsInitialized = true;
            Title += " - New album";            
        }
        private void Close()
        {
            if (OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }
        }

        private void Save()
        {
            // Is this the place? 
            if(CreatedObject.GetType() == typeof(AlbumClass))
            {
                MessageBox.Show("ALBUM");                
            } else
            {
                MessageBox.Show("Slideshow");
            }
        }

        private void Delete()
        {

        }
    }
}
