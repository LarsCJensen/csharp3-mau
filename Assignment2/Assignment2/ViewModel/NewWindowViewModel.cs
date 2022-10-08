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
using FileBase = Assignment2.DAL.Models.FileBase;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Assignment2.BLL.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections;
using System.Windows.Navigation;
using Assignment2.Dialogs.DialogService;
using System.Runtime.ExceptionServices;

namespace Assignment2.ViewModel
{
    public class NewWindowViewModel: BaseViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();
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
        // TODO Validation
        //public string AlbumTitle
        //{
        //    get
        //    {
        //        return _albumManager.Album.Title;
        //    }
        //    set
        //    {
        //        RemoveError(nameof(AlbumTitle));
        //        if (value == null || value.Length == 0)
        //        {
        //            AddError(nameof(AlbumTitle), "You must give the album a name!");
        //        }
        //        _albumManager.Album.Title = value;
        //    }
        //}
        
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
        private ObservableCollection<FileBase> _chosenFiles;
        public ObservableCollection<FileBase> ChosenFiles
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
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand<object> AddCommand { get; private set; }
        public RelayCommand<int> UpCommand { get; private set; }
        public RelayCommand<int> DownCommand { get; private set; }
        public RelayCommand<int> DeleteCommand { get; private set; }
        public RelayCommand ReloadTreeViewCommand { get; private set; }
        public RelayCommand CloseCommand { get; set; }
        #endregion

        #region EventHandlers
        // TODO REMOVE
        //public event EventHandler OnClose;
        public event EventHandler OnSave;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        #endregion

        public NewWindowViewModel()
        {
            // Default constructor
        }
        public NewWindowViewModel(bool slideshow)
        {
            IsSlideshow = slideshow;
            ChosenFiles = new ObservableCollection<FileBase>();
            if (IsSlideshow)
            {
                IsAlbum = false;
                Title = "Home Media Player - New slideshow";
                SlideshowManager = new SlideshowManager();
            } else
            {
                IsAlbum = true;
                Title = "Home Media Player - New album";
                AlbumManager = new AlbumManager();
            }
            RegisterCommands();
            SpinnerVisible = false;            
        }
        public NewWindowViewModel(int id, bool slideshow)
        {
            // Default constructor
            IsSlideshow = slideshow;
            if (IsSlideshow)
            {
                SlideshowManager = new SlideshowManager(id);
                Title = $"Home Media Player - {SlideshowManager.Slideshow.Title}";
                SlideshowManager.Files = new List<SlideshowFile>(SlideshowManager.Slideshow.Files);
            }
            else
            {
                AlbumManager = new AlbumManager(id);
                Title = $"Home Media Player - {AlbumManager.Album.Title}";
                AlbumManager.Files = new List<AlbumFile>(AlbumManager.Album.Files);
            }
            UpdateChosenFiles();
            RegisterCommands();
            SpinnerVisible = false;       
        }        

        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            SelectFolder = new RelayCommand<object>(async param => SelectFolderExcecute(param));
            SaveCommand = new RelayCommand(Save);
            AddCommand = new RelayCommand<object>(param => AddFile(param));
            UpCommand = new RelayCommand<int>(param => Up(param));
            DownCommand = new RelayCommand<int>(param => Down(param));
            DeleteCommand = new RelayCommand<int>(param => Delete(param));
            CloseCommand = new RelayCommand(Close);
            //ReloadTreeViewCommand = new RelayCommand(LoadTreeView);
        }
        // TODO REMOVE
        //public void Close()
        //{
        //    if (OnClose != null)
        //    {
        //        OnClose(this, EventArgs.Empty);
        //    }
        //}
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
        private void Save()
        {
            string validationMessage = "There were validation errors:\n\n";
            if (AlbumManager != null)
            {
                Dictionary<string, string> validationResult = AlbumManager.Save();
                if (validationResult.Count > 0)
                {
                    foreach(KeyValuePair<string, string> entry in validationResult)
                    {
                        validationMessage += $"{entry.Key}: {entry.Value}\n";
                    }
                    DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("Validation errors!", validationMessage);
                    DialogService.OpenDialog(errorVM);
                } else
                {
                    DialogViewModelBase vm = new Dialogs.DialogOk.DialogOkViewModel("Saved!", $"Album {AlbumManager.Album.Title} saved to DB!");
                    DialogService.OpenDialog(vm);
                    OnSave(this, EventArgs.Empty);
                    OnClose(this, EventArgs.Empty);
                }
                
            } else
            {
                Dictionary<string, string> validationResult = SlideshowManager.Save();
                if (validationResult.Count > 0)
                {
                    foreach (KeyValuePair<string, string> entry in validationResult)
                    {
                        validationMessage += $"{entry.Key}: {entry.Value}\n";
                    }

                    DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("Validation errors!", validationMessage);
                    DialogService.OpenDialog(errorVM);
                }
                else
                {
                    DialogViewModelBase vm = new Dialogs.DialogOk.DialogOkViewModel("Saved!", $"Slideshow {SlideshowManager.Slideshow.Title} Saved to DB!");
                    DialogService.OpenDialog(vm);
                    OnSave(this, EventArgs.Empty);
                    OnClose(this, EventArgs.Empty);
                }
                
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
            if (IsAlbum)
            {
                AlbumFile file = new AlbumFile();
                Reflection.CopyProperties(sender, file);
                file.Position = AlbumManager.Files.Count;
                AlbumManager.AddItem(file);
            }
            else if (IsSlideshow)
            {
                SlideshowFile file = new SlideshowFile();
                Reflection.CopyProperties(sender, file);
                file.Position = SlideshowManager.Files.Count;
                SlideshowManager.AddItem(file);
            }
            UpdateChosenFiles();
        }
        private void Up(int selectedIndex)
        {
            if(selectedIndex == 0) 
            {
                DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("Unallowed!", "Can't move the top file up!");
                DialogService.OpenDialog(errorVM);
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
                DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("Unallowed!", "Can't move the bottom file down!");
                DialogService.OpenDialog(errorVM);
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
                DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("An error occured!", @$"Error: {exc.Message}");
                DialogService.OpenDialog(errorVM);
            }
            
            UpdateChosenFiles();
        }
        private void UpdateChosenFiles()
        {
            // TODO Would be nice to have general for both types
            if(AlbumManager != null)
            {
                ChosenFiles = new ObservableCollection<FileBase>(AlbumManager.Files);
            } else
            {
                ChosenFiles = new ObservableCollection<FileBase>(SlideshowManager.Files);
            }
            
        }
        #endregion
        #region Validation
        // FUTURE Not implemented yet!
        public bool HasErrors => _propertyErrors.Any();
        public bool CanSave => !HasErrors;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName, null);
        }
        /// <summary>
        /// Add error message to property
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="error">Error message</param>
        public void AddError(string propertyName, string error)
        {
            if(!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }
            _propertyErrors[propertyName].Add(error);
            OnErrorsChanged(propertyName);  
        }
        public void RemoveError(string propertyName)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(CanSave));
        }
        #endregion

    }
}
