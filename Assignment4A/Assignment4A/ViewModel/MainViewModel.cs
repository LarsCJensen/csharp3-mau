using Assignment4A.BLL;
using Assignment4A.BLL.Enums;
using Assignment4A.BLL.Model;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Assignment4A.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        /// <summary>
        /// Viewmodel for Main
        /// </summary>
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
                BugsCountText = $"{FilteredBugs.Count()}({UnFilteredBugs.Count()}) bugs in the system!";
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
        public List<Developer> Developers { get; set; } = new List<Developer>();     
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
        private CategoryEnum? _selectedCategory;
        public CategoryEnum? SelectedCategory
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
        private StatusEnum? _selectedStatus;
        public StatusEnum? SelectedStatus
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
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return _closeCommand; }
        }
        #endregion
        public bool CanExecute
        {
            get
            {
                // TODO Check stuff?
                return true;
            }
        }
        // Action to show error message
        Action<string> ErrorMessage;
        public MainViewModel()
        {
            CreateDevelopers();
            RegisterCommands();
            Bugs = new ObservableCollection<Bug>();
            // Lambda statement 2
            Bugs.CollectionChanged += (s, e) =>
            {
                BugsCountText = $"{UnFilteredBugs.Count()} bugs in the system!";
            };
            //Lambda expression 3
            ErrorMessage = (s) => ShowErrorMessage(s);
            UnFilteredBugs = new ObservableCollection<Bug>();
            UnFilteredBugs.CollectionChanged += (s, e) =>
            {
                BugsCountText = $"{UnFilteredBugs.Count()} bugs in the system!";
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
            _closeCommand = new CommandHandler(() => ExecuteCloseCommand(), () => CanExecute);

        }
        #region ExecuteCommands
        /// <summary>
        /// Method to execute save bugs command
        /// </summary>
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
                Serializer.XmlFileSerialize<ObservableCollection<Bug>>(_dataFileName, UnFilteredBugs);                    
                _dataDirty = false;
            }
            catch (SerializerException ex)
            {

                ErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.ToString());
            }            
        }
        /// <summary>
        /// Method to execute load bugs command
        /// </summary>
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
                    ErrorMessage(ex.Message);
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.ToString());
                }
            }
        }
        /// <summary>
        /// Method to execute delete bug command
        /// </summary>
        private void ExecuteDeleteCommand()
        {
            if(SelectedBug == null)
            {
                ErrorMessage("Please choose bug to delete!");
                return;
            }

            UnFilteredBugs.Remove(SelectedBug);
            Bugs = UnFilteredBugs;
        }
        /// <summary>
        /// Method to execute filter bug command
        /// </summary
        private void ExecuteFilterCommand()
        {
            if(UnFilteredBugs.Count == 0)
            {
                ErrorMessage("Nothing to filter on!");
                return;
            }
            // Filter on the selected attributes if applicable
            FilteredBugs = new ObservableCollection<Bug>(
                UnFilteredBugs.Where(
                    b => (SelectedCategory == null || b.Category.Equals(SelectedCategory)) 
                    && (SelectedStatus == null || b.Status.Equals(SelectedStatus))
                    && (FilterSearch == null || b.Title.Contains(FilterSearch))
               ).ToList());
            // Bind collection changed to update text 
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
            SelectedCategory = null;
            SelectedStatus = null;
            FilterSearch = "";
        }
        private void ExecuteCloseCommand()
        {
            Close();
        }
        #endregion
        /// <summary>
        /// Helper method for onsave event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="bug"></param>
        public void OnSave(object sender, Bug bug)
        {
            // If bug is new, add it to collection
            if(!UnFilteredBugs.Contains(bug))
            {
                UnFilteredBugs.Add(bug);
            }
            _dataDirty = true;  
            // If bug is solved, show splash and play a jingle!
            if(bug.Status == StatusEnum.Finished)
            {
                SplashScreen splash = new SplashScreen(@"../Assets/horray.png");
                splash.Show(true);
                Uri uri = new Uri(@"pack://application:,,,/Assets/much_rejoicing.wav");
                Stream fileStream = Application.GetResourceStream(uri).Stream;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(fileStream);
                // Play sound from the scene of one of my favorite movies
                player.Play();
                // Wait three seconds and then close splash
                Thread.Sleep(3000);
            } else
            {
                MessageBox.Show("Saved!");
            }
            Bugs = new ObservableCollection<Bug>(UnFilteredBugs);
        }        
        /// <summary>
        /// Helper method for Action
        /// </summary>
        /// <param name="msg">Message to show</param>
        private void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error!");
        }
        /// <summary>
        /// Just a helper function to create developers. Would be separate form
        /// </summary>
        private void CreateDevelopers()
        {
            if (Developers.Count == 0)
            {
                Developers = new List<Developer>()
                {
                   new Developer{ FirstName="Lars", LastName="Jensen", Email="lars.jensen@company.com" },
                   new Developer{ FirstName="John", LastName="Rambo", Email="john.rambo@company.com" },
                   new Developer{ FirstName="Jane", LastName="Seymore", Email="jane.seymore@company.com" },
                };
            }
        }
    }
}
