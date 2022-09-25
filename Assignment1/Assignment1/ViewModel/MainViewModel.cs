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
        
        /// <summary>
        /// Files populating the ListView
        /// </summary>
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
        /// <summary>
        /// Files which are added from ListView to Datagrid
        /// </summary>
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
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand<object> AddCommand { get; private set; }
        public RelayCommand<int> UpCommand { get; private set; }
        public RelayCommand<int> DownCommand { get; private set; }
        public RelayCommand<int> DeleteCommand { get; private set; }
        public RelayCommand ReloadTreeViewCommand { get; private set; }

        #endregion
        #region EventHandlers
        public event EventHandler OnClose;
        #endregion
        public MainViewModel()
        {
            SelectFolder = new RelayCommand<object>(async param => SelectFolderExcecute(param));
            NewCommand = new RelayCommand<string>(param => NewCommandExecute(param));
            SaveCommand = new RelayCommand(Save);
            CloseCommand = new RelayCommand(Close);
            AddCommand = new RelayCommand<object>(param => Add(param));
            UpCommand = new RelayCommand<int>(param => Up(param));
            DownCommand = new RelayCommand<int>(param => Down(param));
            DeleteCommand = new RelayCommand<int>(param => Delete(param));
            //ReloadTreeViewCommand = new RelayCommand(LoadTreeView);
            SpinnerVisible = false;            
        }

        private async void SelectFolderExcecute(object sender)
        {
            SpinnerVisible = true;
            TreeViewItem selecteditem = (TreeViewItem)sender;
            if(Utilities.IsNotNull(selecteditem.Tag))
            {
                await GetFilesInFolder(selecteditem.Tag.ToString());
            }
            
            SpinnerVisible = false;
        }

        private async Task GetFilesInFolder(string tag)
        {
            await Task.Run(() =>
            {                
                List<FileInfo> filesFileInfo = FileUtilities.GetFileInfoFromDirectory(tag, ValidExtensions.AllValidExtensions);
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
            ChosenFiles = new ObservableCollection<ChosenFile>();
            if (commandParam == "album")
            {
                // Change contents of gui?
                Slideshow = null;                
                Album = new Album();
                IsInitialized = true;
                Title = "Home Media Player - New album";
                IsAlbum = true;
                IsSlideshow = false;
            } else
            {
                Album = null;
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
                Title = $"Home Media Player - {Album.Title}";
                MessageBox.Show($"Album {Album.Title} saved!", "Saved!", MessageBoxButton.OK);
            } else
            {
                Title = $"Home Media Player - {Slideshow.Title}";
                MessageBox.Show($"Slideshow {Slideshow.Title} Saved to DB", "Saved!", MessageBoxButton.OK);
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
            if (Album != null)
            {
                Album.MoveItem(selectedIndex, selectedIndex - 1);
            } else if (Slideshow != null) 
            {
                Slideshow.MoveItem(selectedIndex, selectedIndex - 1);
            }
            UpdateChosenFiles();
        }
        private void Down(int selectedIndex)
        {
            if (selectedIndex == ChosenFiles.Count()-1)
            {
                MessageBox.Show("Can't move the bottom file down!", "Unallowed!", MessageBoxButton.OK);
                return;
            }
            if (Album != null)
            {
                Album.MoveItem(selectedIndex, selectedIndex + 1);
            }
            else if (Slideshow != null)
            {
                Slideshow.MoveItem(selectedIndex, selectedIndex + 1);
            }
            UpdateChosenFiles();
        }        
        private void Delete(int selectedIndex)
        {
            try
            {
                if (Album != null)
                {
                    Album.DeleteItem(selectedIndex);
                }
                else if (Slideshow != null)
                {
                    Slideshow.DeleteItem(selectedIndex);
                }
            } catch (Exception exc)
            {
                MessageBox.Show(@$"Error: {exc.Message}");
            }
            
            UpdateChosenFiles();
        }
        private void UpdateChosenFiles()
        {
            // TODO Would be nice to have general for both types
            if(Album != null)
            {
                ChosenFiles = new ObservableCollection<ChosenFile>(Album.Files);
            } else
            {
                ChosenFiles = new ObservableCollection<ChosenFile>(Slideshow.Files);
            }
            
        }

    }
}
