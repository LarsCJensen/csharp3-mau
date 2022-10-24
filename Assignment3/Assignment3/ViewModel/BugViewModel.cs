using Assignment3.BLL;
using Assignment3.BLL.Enums;
using Assignment3.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;

namespace Assignment3.ViewModel
{
    public class BugViewModel: BaseViewModel
    {
        /// <summary>
        /// ViewModel for Bug
        /// </summary>
        private string _windowTitle = "New bug";
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }
        private Bug _bug;
        public Bug Bug
        {
            get { return _bug; }   
            set
            {
                _bug = value;
                OnPropertyChanged("Bug");
            }
        }       
        public List<CategoryEnum> Categories { 
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
                Bug.Category = value;
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
                Bug.Status = value;
                if (value.Equals(StatusEnum.Rejected) || value.Equals(StatusEnum.Finished)) {
                    ShowCloseReason = true;
                } else
                {
                    Bug.CloseReason = String.Empty;
                    ShowCloseReason = false;
                }                
                OnPropertyChanged("SelectedStatus");
            }
        }
        private ObservableCollection<Developer> _developers;
        public ObservableCollection<Developer> Developers
        {
            get { return _developers; }
            set
            {
                _developers = value;
                OnPropertyChanged("Developers");
            }
        }
        private Developer _assignedDeveloper;
        public Developer AssignedDeveloper
        {
            get { return _assignedDeveloper; }
            set
            {
                _assignedDeveloper = value;
                Bug.AssignedDeveloper = value;
                OnPropertyChanged("AssignedDeveloper");
            }
        }
        private string _validationMessage;
        public string ValidationMessage { 
            get { return _validationMessage; }
            set 
            { _validationMessage = value;
                OnPropertyChanged("ValidationMessage");
            } 
        }
        #region Commands
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }
        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get { return _exitCommand; }
        }
        #endregion
        public bool CanExecute
        {
            get
            {
                // TODO Check stuff
                return true;
            }
        }
        private bool _showCloseReason = false;
        public bool ShowCloseReason
        {
            get
            {
                return _showCloseReason;
            }
            set
            {
                _showCloseReason = value;
                OnPropertyChanged("ShowCloseReason");
            }
        }
        // Event for OnSave
        public event EventHandler<Bug> OnSave;
        // Func, perhaps not the best example but couldn't think of a better one :P
        Func<bool> ValidateBug;
        public BugViewModel(List<Developer> developers)
        {
            RegisterCommands();
            Developers = new ObservableCollection<Developer>(developers);            
            ValidateBug = Validate;
            Bug = new Bug();
        }
        public BugViewModel(Bug bug, List<Developer> developers)
        {
            RegisterCommands();
            ValidateBug = Validate;
            WindowTitle = $"Bug: {bug.Title}";
            Developers = new ObservableCollection<Developer>(developers);            
            Bug = bug;
            SelectedCategory = bug.Category;
            SelectedStatus = bug.Status;
            AssignedDeveloper = bug.AssignedDeveloper;
            if(bug.CloseReason != null && bug.CloseReason != "")
            {
                ShowCloseReason = true;
            }            
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            // Lambda Expression 2
            _saveCommand = new CommandHandler(() => Save(), () => CanExecute);
            _exitCommand = new CommandHandler(() => Exit(), () => CanExecute);
        }
        /// <summary>
        /// Helper method for Save
        /// </summary>
        private void Save()
        {
            if(ValidateBug())
            {
                OnSave(this, Bug);
                Exit();
            }        
        }
        /// <summary>
        /// Helper method for Exit
        /// /// </summary>
        private void Exit()
        {
            Close();
        }
        /// <summary>
        /// Helper method for Validate
        /// </summary>
        /// <returns>Bool</returns>
        private bool Validate()
        {
            if(Bug.Title == null || Bug.Title.Length == 0)
            {
                ValidationMessage = "You need to add a title!";
                return false;
            }
            if(Bug.Description == null || Bug.Description.Length == 0)
            {
                ValidationMessage = "You need to add a description!";
                return false;
            }
            if((Bug.Status.Equals(StatusEnum.Rejected) || Bug.Status.Equals(StatusEnum.Finished)) && (Bug.CloseReason == null || Bug.CloseReason.Length == 0))
            {
                ValidationMessage = "You need to provide a closing reason when\nrejecting/finishing a bug!";
                return false;
            }
            ValidationMessage = "";
            return true;
        }        
    }
}
