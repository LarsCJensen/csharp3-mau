using Assignment1_BLL;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ViewModel
{
    public class PlayerViewModel: BaseViewModel
    {
        private string _title; 
        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value; 
                OnPropertyChanged("Title");
            }
        }
        private ObservableCollection<ChosenFile> _slideShowFiles;
        public ObservableCollection<ChosenFile> SlideShowFiles
        {
            get { return _slideShowFiles; }
            private set
            {
                _slideShowFiles = value;
                OnPropertyChanged("SlideShowFiles");
            }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
        private string _videoSource;
        public string VideoSource
        {
            get { return _videoSource; }
            set
            {
                _videoSource = value;
                OnPropertyChanged("VideoSource");
            }
        }
        private bool _isVideo = true;
        public bool IsVideo
        {
            get { return _isVideo; }
            set
            {
                _isVideo = value;
                OnPropertyChanged("IsVideo");
            }
        }

        private bool _isImage = false;
        public bool IsImage
        {
            get { return _isImage; }
            set
            {
                _isImage = value;
                OnPropertyChanged("IsImage");
            }
        }

        #region Commands
        public RelayCommand PlayCommand { get; private set; }
        #endregion

        #region EventHandlers
        public event EventHandler OnClose;
        #endregion
        public PlayerViewModel() 
        {
            
        }
        public PlayerViewModel(string title, List<ChosenFile> chosenFiles)
        {
            Title = title;
            PlayCommand = new RelayCommand(Play);
            SlideShowFiles = new ObservableCollection<ChosenFile>(chosenFiles);
        }
        
        private void Play()
        {
            foreach(ChosenFile file in SlideShowFiles)
            {
                //if(file.Extension)
                //ImageSource = file.Image;
                VideoSource = file.Image;
            }    
        }
    }
}
