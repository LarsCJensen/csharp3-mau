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
                OnPropertyChanged("AssignedDeveloper");
            }
        }
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }
        public bool CanExecute
        {
            get
            {
                // TODO Check stuff?
                return true;
            }
        }
        public event EventHandler<Bug> OnSave;
        public BugViewModel()
        {            
            Bug = new Bug();
            CreateDevelopers();
        }
        public BugViewModel(Bug bug)
        {
            Bug = bug;
            WindowTitle = $"Bug: {bug.Title}";
            CreateDevelopers();
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            _saveCommand = new CommandHandler(() => Save(), () => CanExecute);
        }
        private void Save()
        {
            // TODO Validate
            OnSave(this, Bug);
            Close();               
        }
        private void CreateDevelopers()
        {
            Developers = new ObservableCollection<Developer>()
            {
               new Developer{ FirstName="Lars", LastName="Jensen", Email="lars.jensen@company.com" },
               new Developer{ FirstName="John", LastName="Rambo", Email="john.rambo@company.com" },
               new Developer{ FirstName="Jane", LastName="Seymore", Email="jane.seymore@company.com" },
            };
        }
    }
}
