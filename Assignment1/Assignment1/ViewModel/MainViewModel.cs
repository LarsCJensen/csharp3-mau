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
using System.Reflection;

namespace Assignment1.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        //Hardcoded extensions to search for
        static string[] extensions = new string[] { "*.jpg", "*.bmp", "*.mov", "*.avi", "*.mpg", "*.mp4" };
        
        private string _title = "Home Media Player";
        public string Title
        {
            get { return _title; }
            set { 
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        private bool _spinnerVisible;
        public bool SpinnerVisible
        {
            get
            {
                return _spinnerVisible;
            }
            set
            {
                _spinnerVisible = value;
                OnPropertyChanged("SpinnerVisible");
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
        private ObservableCollection<Assignment1_BLL.File> _files = new ObservableCollection<Assignment1_BLL.File>();
        public ObservableCollection<Assignment1_BLL.File> Files
        {
            get { return _files; }
            private set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }        

        private Album _album;
        public Album Album
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

        private Slideshow _slideshow;
        public Slideshow Slideshow
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
        private ObservableCollection<ChosenFile> _chosenFiles = new ObservableCollection<ChosenFile>();
        public ObservableCollection<ChosenFile> ChosenFiles
        {
            get { return _chosenFiles; }
            private set
            {
                _chosenFiles = value;
                OnPropertyChanged("ChosenFiles");
            }
        }

        #region Commands
        public RelayCommand<object> SelectFolder { get; private set; }
        public RelayCommand<string> NewCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand<object> AddCommand { get; private set; }
        public RelayCommand<int> UpCommand { get; private set; }
        public RelayCommand<int> DownCommand { get; private set; }
        public RelayCommand<int> DeleteCommand { get; private set; }
        public RelayCommand PlayCommand { get; private set; }


        #endregion
        #region EventHandlers
        public event EventHandler OnClose;
        #endregion
        public MainViewModel()
        {
            SelectFolder = new RelayCommand<object>(async param => SelectFolderExcecute(param));
            NewCommand = new RelayCommand<string>(param => NewCommandExecute(param));
            SaveCommand = new RelayCommand(Save);
            AddCommand = new RelayCommand<object>(param => Add(param));
            UpCommand = new RelayCommand<int>(param => Up(param));
            DownCommand = new RelayCommand<int>(param => Down(param));
            DeleteCommand = new RelayCommand<int>(param => Delete(param));
            PlayCommand = new RelayCommand(PlaySlideshow);
            SpinnerVisible = false;
        }

        private async void SelectFolderExcecute(object sender)
        {
            //// TODO Try catch!
            //TreeViewItem selecteditem = (TreeViewItem)sender;
            //string[] extensions = new string[] { "*.jpg", "*.bmp", "*.mov", "*.avi", "*.mpg", "*.mp4" };

            //List<FileInfo> filesFileInfo = FileUtilities.GetFileInfoFromDirectory(selecteditem.Tag.ToString(), extensions);
            //Files = new ObservableCollection<Assignment1_BLL.File>();
            //foreach (FileInfo file in filesFileInfo)
            //{
            //    Assignment1_BLL.File newFile = new Assignment1_BLL.File(file);
            //    Files.Add(newFile);
            //}
            SpinnerVisible = true;
            TreeViewItem selecteditem = (TreeViewItem)sender;
            await GetFilesInFolder(selecteditem.Tag.ToString());
            SpinnerVisible = false;
        }

        private async Task GetFilesInFolder(string tag)
        {
            await Task.Run(() =>
            {                
                List<FileInfo> filesFileInfo = FileUtilities.GetFileInfoFromDirectory(tag, extensions);
                Files = new ObservableCollection<Assignment1_BLL.File>();
                foreach (FileInfo file in filesFileInfo)
                {
                    Assignment1_BLL.File newFile = new Assignment1_BLL.File(file);
                    Files.Add(newFile);
                }
            });
        }
        private void NewCommandExecute(string commandParam)
        {
            if(commandParam == "album")
            {
                // Change contents of gui?
                Album = new Album();
                IsInitialized = true;
                Title = "Home Media Player - New album";
                IsAlbum = true;
                IsSlideshow = false;
            } else
            {
                Slideshow = new Slideshow();
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
                MessageBox.Show($"Album {Album.Name} saved!", "Unallowed!", MessageBoxButton.OK);
            } else
            {
                Title = $"Home Media Player - {Slideshow.Name}";
                MessageBox.Show($"Slideshow {Slideshow.Name} Saved to DB", "Unallowed!", MessageBoxButton.OK);
            }
        }
        private void Add(object sender)
        {
            if(sender == null)
            {
                MessageBox.Show("Please choose file to add!", "Error!", MessageBoxButton.OK);
                return;
            }
            
            
            if (Album != null)
            {
                ChosenFile file = new ChosenFile((Assignment1_BLL.File)sender, Album.Files.Count());
                Album.AddItem(file);                
            }
            else if (Slideshow != null)
            {
                ChosenFile file = new ChosenFile((Assignment1_BLL.File)sender, Slideshow.Files.Count());
                Slideshow.AddItem(file);
            }
            UpdateChosenFiles();
        }
        private void Up(int selectedIndex)
        {
            if(selectedIndex == 0) 
            {
                MessageBox.Show("Can't move the top file up!", "Unallowed!", MessageBoxButton.OK);
                return;
            }
            ChosenFiles.Move(selectedIndex, selectedIndex - 1);
        }
        private void Down(int selectedIndex)
        {
            if (selectedIndex == ChosenFiles.Count()-1)
            {
                MessageBox.Show("Can't move the bottom file down!", "Unallowed!", MessageBoxButton.OK);
                return;
            }
            ChosenFiles.Move(selectedIndex, selectedIndex + 1);
        }        
        private void Delete(int selectedIndex)
        {
            if (Album != null)
            {
                Album.DeleteItem(selectedIndex);
            }
            else if (Slideshow != null)
            {
                Slideshow.DeleteItem(selectedIndex);
            }
            UpdateChosenFiles();
        }
        private void UpdateChosenFiles()
        {
            // TODO Would be nice to have generic
            if(Album != null)
            {
                ChosenFiles = new ObservableCollection<ChosenFile>(Album.Files);
            } else
            {
                ChosenFiles = new ObservableCollection<ChosenFile>(Slideshow.Files);
            }
            
        }

        private void PlaySlideshow()
        {
            // TODO Not used
        }
    }
}
