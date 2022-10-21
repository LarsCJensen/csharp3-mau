using Assignment3.BLL;
using Assignment3.BLL.Enums;
using Assignment3.BLL.Model;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Utilities;

namespace Assignment3.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        // TODO Is this needed?
        public BugManager BugManager = new BugManager();
        private bool _dataDirty;
        private string _dataFileName;
        public ObservableCollection<Bug> _bugs;
        public ObservableCollection<Bug> Bugs
        {
            get { return _bugs; }   
            set 
            { 
                _bugs = value;
                OnPropertyChanged("Bugs");
            }
        }
        public ObservableCollection<Bug> _unFilteredBugs;
        public ObservableCollection<Bug> UnFilteredBugs
        {
            get { return _unFilteredBugs; }
            set
            {
                _unFilteredBugs = value;
                BugsCountText = $"{UnFilteredBugs.Count()} bugs in the system!";                
            }
        }
        public ObservableCollection<Bug> _filteredBugs;
        public ObservableCollection<Bug> FilteredBugs
        {
            get { return _filteredBugs; }
            set
            {
                _filteredBugs = value;
                BugsCountText = $"{FilteredBugs.Count()}({Bugs.Count()}) bugs in the system!";
            }
        }
        private string _title = "You got bugs!";
        public string Title {
            get { return _title;  }
            set 
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        private string _bugsCountText = "0 bugs in the system";
        public string BugsCountText
        {
            get
            {
                return _bugsCountText;
            }
            set
            {
                _bugsCountText = value;
                OnPropertyChanged("BugsCountText");
            }
        }
        private Bug _selectedBug;
        public Bug SelectedBug
        {
            get { return _selectedBug; }
            set
            {
                _selectedBug = value;
                OnPropertyChanged("SelectedBug");
            }
        }
        public List<CategoryEnum> Categories
        {
            get
            {
                return Enum.GetValues(typeof(CategoryEnum)).Cast<CategoryEnum>().ToList();
            }
        }
        private CategoryEnum _selectedCategory;
        public CategoryEnum SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        public List<StatusEnum> Status
        {
            get
            {
                return Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().ToList();
            }
        }
        private StatusEnum _selectedStatus;
        public StatusEnum SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                _selectedStatus = value;
                OnPropertyChanged("SelectedStatus");
            }
        }
        private string _filterSearch;
        public string FilterSearch
        {
            get { return _filterSearch; }
            set
            {
                _filterSearch = value;
                OnPropertyChanged("FilterSearch");
            }
        }
        #region Commands
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }
        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get { return _loadCommand; }
        }
        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
        }
        private ICommand _filterCommand;
        public ICommand FilterCommand
        {
            get { return _filterCommand; }
        }
        private ICommand _clearFilterCommand;
        public ICommand ClearFilterCommand
        {
            get { return _clearFilterCommand; }
        }
        public bool CanExecute
        {
            get
            {
                // TODO Check stuff?
                return true;
            }
        }
        #endregion
        public MainViewModel()
        {
            RegisterCommands();
            Bugs = new ObservableCollection<Bug>();
            // Lambda statement 2
            Bugs.CollectionChanged += (s, e) =>
            {
                BugsCountText = $"{Bugs.Count()} bugs in the system!";
            };
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            // Lambda Statement 1
            _saveCommand = new CommandHandler(() => ExecuteSaveCommand(), () => CanExecute);
            _loadCommand = new CommandHandler(() => ExecuteLoadCommand(), () => CanExecute);
            _deleteCommand = new CommandHandler(() => ExecuteDeleteCommand(), () => CanExecute);
            _filterCommand = new CommandHandler(() => ExecuteFilterCommand(), () => CanExecute);
            _clearFilterCommand = new CommandHandler(() => ExecuteClearFilterCommand(), () => CanExecute);

        }
        #region ExecuteCommands
        private void ExecuteSaveCommand()
        {
            if(_dataFileName == null)
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = "XML|*.xml|All files (*.*)|*.*";
                bool? result = dialog.ShowDialog();
                if (result == true)
                {
                    _dataFileName = dialog.FileName;
                }
            }
            try
            {
                Serializer.XmlFileSerialize<ObservableCollection<Bug>>(_dataFileName, Bugs);                    
                _dataDirty = false;
            }
            catch (SerializerException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        public void ExecuteLoadCommand()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML|*.xml|All files (*.*)|*.*";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    UnFilteredBugs = new ObservableCollection<Bug>(Serializer.XmlFileDeSerialize<ObservableCollection<Bug>>(dialog.FileName));
                    // I need to bind collection changed to this new instance of the observable collection
                    UnFilteredBugs.CollectionChanged += (s, e) =>
                    {
                        BugsCountText = $"{UnFilteredBugs.Count()} bugs in the system!";
                    };
                    _dataFileName = dialog.FileName;
                    _dataDirty = false;
                    Bugs = UnFilteredBugs;
                }
                catch (SerializerException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ExecuteDeleteCommand()
        {
            UnFilteredBugs.Remove(SelectedBug);
            Bugs = UnFilteredBugs;
        }

        private void ExecuteFilterCommand()
        {
            // SelectedCategory
            //SelectedStatus
            //FilterSearch

            FilteredBugs = new ObservableCollection<Bug>(Bugs.Where(b => b.Category.Equals(SelectedCategory)).ToList());
            FilteredBugs.CollectionChanged += (s, e) =>
            {
                BugsCountText = $"{FilteredBugs.Count()}({UnFilteredBugs.Count()}) bugs in the system!";
            };
            Bugs = FilteredBugs;

        }
        private void ExecuteClearFilterCommand()
        {
            Bugs = UnFilteredBugs;
            BugsCountText = $"{UnFilteredBugs.Count()} bugs in the system!";
        }
        #endregion
        public void OnSave(object sender, Bug bug)
        {
            // TODO Check if it is checked off, perhaps play a jingle?            
            // TODO  Check if bug already is in the list
            if(!UnFilteredBugs.Contains(bug))
            {
                UnFilteredBugs.Add(bug);
            }
            _dataDirty = true;
            if(bug.Status == StatusEnum.Finished)
            {
                SplashScreen splash = new SplashScreen(@"../Assets/horray.png");
                splash.Show(true);
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = @"../Assets/much_rejoicing.wav";
                // Play sound from the scene of one of my favorite movies
                player.Play();
                // Wait three seconds and then close splash
                Thread.Sleep(3000);
            } else
            {
                MessageBox.Show("Saved!");
            }
            Bugs = UnFilteredBugs;
        }        
    }
    
    // TODO Bind to event on save in bugs view

}
