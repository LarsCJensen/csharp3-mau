using Assignment2.BLL;
using Assignment2.DAL.Models;
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
    public class ListViewModel: BaseViewModel
    {
        public string Title { get; set; } = "Albums/Slideshows";
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
                _selectedIndex = value;
                EditDeleteActive = false;
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
                //SelectedSlideshow = null;
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
                //SelectedAlbum = null;
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
        public string SearchText { get; set; }
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

        #region Commands
        public RelayCommand OpenAbout { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public event EventHandler OnClose;

        #endregion
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

    }
        private void OpenAboutExecute()
        {
            // TODO Open About
        }
        public void Close()
        {
            if (OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }
        }
        private void DeleteExecute()
        {
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

        public void OnSave(object sender, EventArgs e)
        {
            LoadList(SelectedIndex);
        }
    }
}
