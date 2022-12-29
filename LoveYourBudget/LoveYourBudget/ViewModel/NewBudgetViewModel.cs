using GalaSoft.MvvmLight.Command;
using LoveYourBudget.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.ViewModel
{
    public class NewBudgetViewModel: BaseViewModel
    {
        public List<String> Years { 
            get
            {
                return new List<String>()
                {
                    "2023", "2022"
                };
            }        
        }
        public List<String> Months
        {
            get
            {
                return new List<String>()
                {
                    "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                };
            }
        }

        private BudgetManager _budgetManager;
        public BudgetManager BudgetManager
        {
            get
            {
                return _budgetManager;
            }
            set
            {
                _budgetManager = value;
                OnPropertyChanged("BudgetManager");
            }
        }
        public string Title
        {
            get { 
                return _budgetManager.Budget.Year + "-" + _budgetManager.Budget.Month; 
            }            
        }
        private string _selectedYear;
        public string SelectedYear
        {
            get
            {
                return _selectedYear;
            }
            set
            {
                _selectedYear = value;
                OnPropertyChanged("SelectedYear");
            }
        }
        private string _selectedMonth;
        public string SelectedMonth
        {
            get
            {
                return _selectedMonth;
            }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged("SelectedMonth");
            }
        }
        #region EventHandlers
        public event EventHandler OnSave;
        #endregion
        #region Commands
        public RelayCommand SaveCommand { get; private set; }
        #endregion
        public NewBudgetViewModel() 
        {
            _budgetManager= new BudgetManager();
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            SaveCommand = new RelayCommand(Save);
        }
        private void Save()
        {
            _budgetManager.Save();
            OnSave(this, EventArgs.Empty);
        }
    }
}
