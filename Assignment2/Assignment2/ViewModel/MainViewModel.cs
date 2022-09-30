using Assignment2.Utilities;
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
using Assignment2.BLL;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection;
using Assignment2.BLL.Services;
using Assignment2.DAL.Models;
using File = Assignment2.DAL.Models.File;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Assignment2.ViewModel
{
    public class MainViewModel: BaseViewModel, IDataErrorInfo
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
        private ObservableCollection<FileInfo> _files = new ObservableCollection<FileInfo>();
        public ObservableCollection<FileInfo> Files
        {
            get { return _files; }
            private set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }        

        private AlbumManager _albumManager;
        public AlbumManager AlbumManager
        {
            get
            {
                return _albumManager;
            }
            set
            {
                _albumManager = value;
                OnPropertyChanged("AlbumManager");
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

        private SlideshowManager _slideshowManager;
        public SlideshowManager SlideshowManager
        {
            get
            {
                return _slideshowManager;
            }
            set
            {
                _slideshowManager = value;
                OnPropertyChanged("SlideshowManager");
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
        private ObservableCollection<File> _chosenFiles;
        public ObservableCollection<File> ChosenFiles
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

        #region IDataErrorInfo
        public string Error => throw new NotImplementedException();
        /// <summary>
        /// Validation of properties
        /// </summary>
        /// <param name="property">Property to validate</param>
        /// <returns>string</returns>
        public string this[string property]
        {
            get
            {
                string validationResult = String.Empty;
                switch (property)
                {
                    case "Title":
                        validationResult = "Wrong!";
                        break;
                    case "Description":
                        validationResult = "Description WRONG!";                        
                        break;
                }
                return validationResult;
            }
        }
        #endregion
        public MainViewModel()
        {
            RegisterCommands();
            SpinnerVisible = false;            
        }

        protected override void RegisterCommands()
        {
            SelectFolder = new RelayCommand<object>(async param => SelectFolderExcecute(param));
            NewCommand = new RelayCommand<string>(param => NewCommandExecute(param));
            SaveCommand = new RelayCommand(Save);
            CloseCommand = new RelayCommand(Close);
            AddCommand = new RelayCommand<object>(param => AddFile(param));
            UpCommand = new RelayCommand<int>(param => Up(param));
            DownCommand = new RelayCommand<int>(param => Down(param));
            DeleteCommand = new RelayCommand<int>(param => Delete(param));
            //ReloadTreeViewCommand = new RelayCommand(LoadTreeView);
        }

        private async void SelectFolderExcecute(object sender)
        {
            SpinnerVisible = true;
            TreeViewItem selecteditem = (TreeViewItem)sender;
            if(Utilities.Utilities.IsNotNull(selecteditem.Tag))
            {
                await GetFilesInFolder(selecteditem.Tag.ToString());
            }
            
            SpinnerVisible = false;
        }

        private async Task GetFilesInFolder(string tag)
        {
            await Task.Run(() =>
            {
                Files = new ObservableCollection<FileInfo>();
                List<FileInfo> filesFileInfo = FileUtilities.GetFileInfoFromDirectory(tag, ValidExtensions.AllValidExtensions);
                foreach (FileInfo file in filesFileInfo)
                {
                    // Since Files collection is on UI thread it needs to be delegated
                    App.Current.Dispatcher.Invoke((Action)delegate 
                    {
                        Files.Add(file);
                    });
                }
            });
        }
        private void NewCommandExecute(string commandParam)
        {
            ChosenFiles = new ObservableCollection<File>();
            if (commandParam == "album")
            {
                // Change contents of gui?
                SlideshowManager = null;
                AlbumManager = new AlbumManager();
                IsInitialized = true;
                Title = "Home Media Player - New album";
                IsAlbum = true;
                IsSlideshow = false;
            } else
            {
                AlbumManager = null;
                SlideshowManager = new SlideshowManager();
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
            if(AlbumManager != null)
            {
                // Save to db
                Title = $"Home Media Player - {AlbumManager.Album.Title}";
                // TODO Handle
                bool result = AlbumManager.Save();
                MessageBox.Show($"Album {AlbumManager.Album.Title} saved!", "Saved!", MessageBoxButton.OK);
            } else
            {
                Title = $"Home Media Player - {SlideshowManager.Slideshow.Title}";
                // TODO Handle
                bool result = SlideshowManager.Save();
                MessageBox.Show($"Slideshow {SlideshowManager.Slideshow.Title} Saved to DB", "Saved!", MessageBoxButton.OK);
            }
        }
        #region CommandExecutes
        private void AddFile(object sender)
        {
            if(sender == null)
            {
                MessageBox.Show("Please choose file to add!", "Error!", MessageBoxButton.OK);
                return;
            }
            File file = new File();
            Reflection.CopyProperties(sender, file);
            if (AlbumManager != null)
            {
                AlbumManager.AddItem(file);
            }
            else if (SlideshowManager != null)
            {
                SlideshowManager.AddItem(file);
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
            if (AlbumManager != null)
            {
                AlbumManager.MoveItem(selectedIndex, selectedIndex - 1);
            } else if (SlideshowManager != null) 
            {
                SlideshowManager.MoveItem(selectedIndex, selectedIndex - 1);
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
            if (AlbumManager != null)
            {
                AlbumManager.MoveItem(selectedIndex, selectedIndex + 1);
            }
            else if (SlideshowManager != null)
            {
                SlideshowManager.MoveItem(selectedIndex, selectedIndex + 1);
            }
            UpdateChosenFiles();
        }        
        private void Delete(int selectedIndex)
        {
            try
            {
                if (AlbumManager != null)
                {
                    AlbumManager.DeleteItem(selectedIndex);
                }
                else if (SlideshowManager != null)
                {
                    SlideshowManager.DeleteItem(selectedIndex);
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
            if(AlbumManager != null)
            {
                ChosenFiles = new ObservableCollection<File>(AlbumManager.Files);
            } else
            {
                ChosenFiles = new ObservableCollection<File>(SlideshowManager.Files);
            }
            
        }
        #endregion
    }
}
