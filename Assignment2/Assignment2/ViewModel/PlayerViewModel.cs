using Assignment2.BLL;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Utilities;
using System.Windows;
using System.Diagnostics;
using Assignment2.DAL.Models;
using Assignment2.BLL.Model;

namespace Assignment2.ViewModel
{
    public class PlayerViewModel: BaseViewModel
    {        
        private string _title = "Slideshow player"; 
        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value; 
                OnPropertyChanged("Title");
            }
        }
        private ObservableCollection<FileBase> _slideShowFiles;
        public ObservableCollection<FileBase> SlideShowFiles
        {
            get { return _slideShowFiles; }
            private set
            {
                _slideShowFiles = value;
                OnPropertyChanged("SlideShowFiles");
            }
        }
        private int _delay;
        public int Interval
        {
            get { return _delay; }
            set
            {
                _delay = value;               
            }
        }
        public bool Continue { get; set; } = true;
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
        private bool _isVideo = false;
        public bool IsVideo
        {
            get { return _isVideo; }
            set
            {
                _isVideo = value;
                OnPropertyChanged("IsVideo");
            }
        }

        private bool _isImage = true;
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
        public RelayCommand PauseCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
        public RelayCommand ForwardCommand { get; private set; }
        #endregion

        #region EventHandlers
        public event EventHandler OnClose;
        #endregion
        public PlayerViewModel() { }
        public PlayerViewModel(string title, ICollection<SlideshowFile> slideshowFiles, int interval)
        {
            if(title != null)
            {
                Title = title;
            }            
            Interval = interval;
            PlayCommand = new RelayCommand(Play);
            PauseCommand = new RelayCommand(Pause);
            BackCommand = new RelayCommand(Back);
            ForwardCommand = new RelayCommand(Forward);
            
            SlideShowFiles = new ObservableCollection<FileBase>(slideshowFiles);
        }
        /// <summary>
        /// Play slideshow
        /// </summary>
        private async void Play()
        {
            ImageSource = null;
            VideoSource = null;
            foreach (FileBase file in SlideShowFiles)
            {
                if (Utilities.Utilities.IsNull(file.Extension) || Utilities.Utilities.IsNull(file.FullName))
                {
                    continue;
                }
                // An "ugly" work-around to match extension
                if (ValidExtensions.ImageExtensions.Contains($"*{file.Extension.ToLower()}"))
                {
                    if (Utilities.Utilities.IsNotNull(ImageSource))
                    {
                        await Task.Delay(Interval * 1000);
                    }
                    IsVideo = false;
                    IsImage = true;
                    ImageSource = file.FullName;
                    await Task.Delay(Interval * 1000);
                }
                                                                    // An "ugly" work-around to match extension
                else if (ValidExtensions.VideoExtensions.Contains($"*{file.Extension.ToLower()}"))
                {
                    IsImage = false;
                    IsVideo = true;
                    Continue = false;
                    // Get length of Video
                    int videoLength = (int)Math.Ceiling(Utilities.Utilities.GetVideoDuration(file.FullName));
                    VideoSource = file.FullName;
                    await Task.Delay(videoLength);                    
                }
            }
            // TODO
            // Would have implemented custom dialog for this
            MessageBox.Show("Slideshow done!", "Done!", MessageBoxButton.OK);
        }
        private void Pause()
        {
            NotImplementedMessage();
        }
        private void Forward()
        {
            NotImplementedMessage();
        }
        private void Back()
        {
            NotImplementedMessage();
        }

        private void NotImplementedMessage()
        {
            MessageBox.Show("Not implemented yet!", "Not implemented!", MessageBoxButton.OK);
        }
    }
}
