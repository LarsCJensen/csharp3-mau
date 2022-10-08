using Assignment2.BLL;
using Assignment2.BLL.Model;
using Assignment2.DAL.Models;
using Assignment2.Dialogs.DialogService;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Assignment2.ViewModel
{
    public class ListViewModel : BaseViewModel
    {
        /// <summary>
        /// ViewModel for ListView, which is the main view
        /// </summary>
        public string Title { get; } = "Albums/Slideshows";
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
        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                // When changing tabs, certain properties should be reset
                _selectedIndex = value;
                EditDeleteActive = false;
                SearchText = string.Empty;
                SearchResultVisible = false;
                LoadList(value);
                OnPropertyChanged("SelectedIndex");
            }
        }
        private Album _selectedAlbum;
        public Album SelectedAlbum
        {
            get
            {
                return _selectedAlbum;
            }
            set
            {
                _selectedAlbum = value;
                EditDeleteActive = true;
                OnPropertyChanged("SelectedAlbum");
            }
        }
        private Slideshow _selectedSlideshow;
        public Slideshow SelectedSlideshow
        {
            get
            {
                return _selectedSlideshow;
            }
            set
            {
                _selectedSlideshow = value;
                EditDeleteActive = true;
                OnPropertyChanged("SelectedSlideshow");
            }
        }
        private bool _isSlideshow;
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
        private string _searchText;
        public string SearchText { 
            get
            {
                return _searchText;
            } 
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        private ObservableCollection<Album> _albums = new ObservableCollection<Album>();
        public ObservableCollection<Album> Albums
        {
            get
            {
                return _albums;
            }
            set
            {
                _albums = value;
                OnPropertyChanged("Albums");
            }
        }
        private ObservableCollection<Slideshow> _slideshows = new ObservableCollection<Slideshow>();
        public ObservableCollection<Slideshow> Slideshows
        {
            get
            {
                return _slideshows;
            }
            set
            {
                _slideshows = value;
                OnPropertyChanged("Slideshows");
            }
        }
        // An ugly way to have which properties to search for
        public List<string> SearchProperties 
        { 
            get
            {
                return new List<string>() { "Title", "Description", "Filename" };
            }
        }
        private string _selectedProperty;
        public string SelectedProperty
        {
            get
            {
                return _selectedProperty;
            }
            set
            {
                _selectedProperty = value;
                OnPropertyChanged("SelectedProperty");
            }
        }
        private bool _editDeleteActive;
        public bool EditDeleteActive
        {
            get { return _editDeleteActive; }
            set
            {
                _editDeleteActive = value;
                OnPropertyChanged("EditDeleteActive");
            }
        }
        private bool _searchResultVisible;
        public bool SearchResultVisible
        {
            get
            {
                return _searchResultVisible;
            }
            set
            {
                _searchResultVisible = value;
                OnPropertyChanged("SearchResultVisible");
            }
        }
        private string _searchResult;
        public string SearchResult
        {
            get
            {
                return _searchResult;
            }
            set
            {
                _searchResult = value;
                OnPropertyChanged("SearchResult");
            }
        }

        #region Commands
        public RelayCommand OpenAbout { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        #endregion
        //#region EventHandlers
        //public event EventHandler OnClose;
        //#endregion
        public ListViewModel()
        {
            AlbumManager = new AlbumManager();
            SlideshowManager = new SlideshowManager();
            RegisterCommands();
            LoadList(SelectedIndex);            
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            OpenAbout = new RelayCommand(OpenAboutExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            CloseCommand = new RelayCommand(Close);
            SearchCommand = new RelayCommand(SearchExecute);

    }
        private void OpenAboutExecute()
        {            
            string aboutMessage = "To create a new album use new Album button.";
            aboutMessage += "\nTo create a new slideshow use New Slideshow button.";
            aboutMessage += $"\n\nBrowse for files through the tree view.\nSupported file types are {String.Join(", ", ValidExtensions.AllValidExtensions)}.";
            aboutMessage += "\n\nFor slideshows you can choose interval to be used between images. \nVideos will be played in its full length.";

            DialogViewModelBase aboutVM = new Dialogs.DialogOk.DialogOkViewModel("About Home Media Player!", aboutMessage);
            DialogService.OpenDialog(aboutVM);

        }
        // TODO REMOVE
        //public void Close()
        //{
        //    if (OnClose != null)
        //    {
        //        OnClose(this, EventArgs.Empty);
        //    }
        //}
        /// <summary>
        /// Handler for Delete command
        /// </summary>
        private void DeleteExecute()
        {
            // TODO
            if(SelectedAlbum != null)
            {
                bool result = AlbumManager.Delete(SelectedAlbum.id);
            }
            if (SelectedSlideshow != null)
            {
                bool result = SlideshowManager.Delete(SelectedSlideshow.id);
            }
            LoadList(SelectedIndex);
        }
        /// <summary>
        /// Handler for SearchCommand
        /// </summary>
        private void SearchExecute()
        {
            if(SearchText == null || SearchText == string.Empty)
            {
                DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("No search text!", "You must enter text to search for!");
                DialogService.OpenDialog(errorVM);
                return;
            } 
            if(SelectedProperty == null)
            {
                DialogViewModelBase errorVM = new Dialogs.DialogOk.DialogOkViewModel("No property selected!", "You must choose property to search in!");
                DialogService.OpenDialog(errorVM);
                return;
            }
            SearchResult = "Number of matches: ";
            if (SelectedIndex == 0)
            {                
                Albums = new ObservableCollection<Album>(AlbumManager.SearchItems(SearchText, SelectedProperty));
                IsSlideshow = false;
                SearchResult += Albums.Count().ToString();
            }else if(SelectedIndex == 1)
            {
                Slideshows = new ObservableCollection<Slideshow>(SlideshowManager.SearchItems(SearchText, SelectedProperty));
                IsSlideshow = true;
                
                SearchResult += Slideshows.Count().ToString();
            }
            SearchResultVisible = true;
        }
        /// <summary>
        /// Helper method to load list of items
        /// </summary>
        /// <param name="selectedIndex">Which tab that is active</param>
        public void LoadList(int selectedIndex)
        {
            switch(selectedIndex)
            {
                case 0:
                    Albums = new ObservableCollection<Album>(AlbumManager.GetItems());
                    IsSlideshow = false;
                    break;
                case 1:
                    Slideshows = new ObservableCollection<Slideshow>(SlideshowManager.GetItems());
                    IsSlideshow = true;
                    break;
            }
        }
        /// <summary>
        /// Handler for OnSave event. Used to reload list after album is added/edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSave(object sender, EventArgs e)
        {
            LoadList(SelectedIndex);
        }
    }
}
