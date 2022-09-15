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

        private AlbumClass _album;
        public AlbumClass Album
        {
            get
            {
                return _album;
            }
            set
            {
                _album = value;
                OnPropertyChanged("Album");
            }
        }
        
        private bool _isAlbum = true;
        public bool IsAlbum
        {
            get
            {
                return _isAlbum;
            }
            set
            {
                _isAlbum = value;
                OnPropertyChanged("IsAlbum");
            }
        }

        private SlideshowClass _slideshow;
        public SlideshowClass Slideshow
        {
            get
            {
                return _slideshow;
            }
            set
            {
                _slideshow = value;
                OnPropertyChanged("Slideshow");
            }
        }

        private bool _isSlideshow = false;
        public bool IsSlideshow
        {
            get
            {
                return _isSlideshow;
            }
            set
            {
                _isSlideshow = value;
                OnPropertyChanged("IsSlideshow");
            }
        }
        private ObservableCollection<FileClass> _chosenFiles = new ObservableCollection<FileClass>();
        public ObservableCollection<FileClass> ChosenFiles
        {
            get { return _chosenFiles; }
            private set
            {
                _chosenFiles = value;
                //OnPropertyChanged("ChosenFiles");
            }
        }

        #region Commands
        public RelayCommand<object> SelectFolder { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand<string> NewCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand<object> AddCommand { get; private set; }
        public RelayCommand<int> UpCommand { get; private set; }
        public RelayCommand<int> DownCommand { get; private set; }
        public RelayCommand<int> DeleteCommand { get; private set; }

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
            AddCommand = new RelayCommand<object>(param => Add(param));
            UpCommand = new RelayCommand<int>(param => Up(param));
            DownCommand = new RelayCommand<int>(param => Down(param));
            DeleteCommand = new RelayCommand<int>(param => Delete(param));
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
            if(commandParam == "album")
            {
                // Change contents of gui?
                Album = new AlbumClass();
                IsInitialized = true;
                Title = "Home Media Player - New album";
                IsAlbum = true;
                IsSlideshow = false;
            } else
            {
                
                IsInitialized = true;
                Title = "Home Media Player - New slideshow";
                IsSlideshow = true;
                IsAlbum = false;
            }
            
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
            if(Album != null)
            {
                // Save to db
                Title = $"Home Media Player - {Album.Name}";
                MessageBox.Show("Saved to DB");
            } else
            {
                Title = $"Home Media Player - {Slideshow.Name}";
                MessageBox.Show("Saved to DB");
            }
        }
        private void Add(object sender)
        {
            if(sender == null)
            {
                MessageBox.Show("Please choose file to add!");
                return;
            }
            // TODO We don't want the list in ALbum class to be observable?
            FileClass file = (FileClass)sender;
            if (Album != null)
            {                
                Album.AddFile(file);                
            }
            ChosenFiles.Add(file);
        }
        private void Up(int selectedIndex)
        {
            if(selectedIndex == 0) 
            {
                MessageBox.Show("Can't move the top file up!");
                return;
            }
            ChosenFiles.Move(selectedIndex, selectedIndex - 1);
        }
        private void Down(int selectedIndex)
        {
            if (selectedIndex == ChosenFiles.Count()-1)
            {
                MessageBox.Show("Can't move the bottom file down!");
                return;
            }
            ChosenFiles.Move(selectedIndex, selectedIndex + 1);
        }        
        private void Delete(int selectedIndex)
        {
            if (selectedIndex == null)
            {
                MessageBox.Show("You must choose a file to delete!");
            }
        }
    }
}
