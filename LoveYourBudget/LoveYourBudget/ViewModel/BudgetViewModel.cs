using GalaSoft.MvvmLight.Command;
using LoveYourBudget.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls.Ribbon.Primitives;

namespace LoveYourBudget.ViewModel
{
    public class BudgetViewModel: BaseViewModel
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
                    "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"
                };
            }
        }
        #region Properties
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
        private string _title;
        public string Title
        {
            get {
                return _title; 
            }            
            set
            {
                _title = value;
                OnPropertyChanged("Title");
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
        private string _amount;
        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }
        private ObservableCollection<BudgetRow> _budgetRows = new ObservableCollection<BudgetRow>();
        public ObservableCollection<BudgetRow> BudgetRows
        {
            get
            {
                return _budgetRows;
            }
            set
            {
                _budgetRows = value;
                OnPropertyChanged("BudgetRows");
            }
        }
        private BudgetRow _selectedBudgetRow;
        public BudgetRow SelectedBudgetRow
        {
            get
            {
                return _selectedBudgetRow;
            }
            set
            {
                _selectedBudgetRow = value;
                OnPropertyChanged("SelectedBudgetRow");
            }
        }
        #endregion
        #region EventHandlers
        public event EventHandler OnSave;
        #endregion
        #region Commands
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand<BudgetRow> DeleteCommand { get; private set; }
        #endregion
        public BudgetViewModel()
        {
            BudgetManager = new BudgetManager();
            Title = "New budget";
            LoadCategories();
        }
        public BudgetViewModel(string year, string month) 
        {
            BudgetManager= new BudgetManager();
            BudgetManager.Budget.Year = year;
            BudgetManager.Budget.Month = month;
            SelectedYear = year;
            SelectedMonth = month;             
            Title = "New budget";
            LoadCategories();
        }
        public BudgetViewModel(BudgetManager budgetManager)
        {
            BudgetManager = budgetManager;
            Title = BudgetManager.Budget.Year + "-" + BudgetManager.Budget.Month;
            BudgetRows = new ObservableCollection<BudgetRow>(BudgetManager.BudgetRows);
            LoadCategories();
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            AddCommand = new RelayCommand(Add);
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand<BudgetRow>(param => Delete(param));
        }
        private void Save()
        {
            // TODO Save data in separate thread
            BudgetManager.SaveBudget();
            OnSave(this, EventArgs.Empty);
            Close();
        }
        private void Add()
        {
            // TODO Validate
            Double.TryParse(Amount, out double result);
            BudgetRow budgetRow = new BudgetRow() 
            {
                CreatedTime =  DateTime.Now,
                CategoryId = SelectedCategory.Id,
                Amount = result,
            };
            BudgetManager.BudgetRows.Add(budgetRow);
            SelectedCategory = null;
            Amount = "";
            BudgetRows = new ObservableCollection<BudgetRow>(BudgetManager.BudgetRows);
        }
        private void LoadCategories()
        {
            Categories = new ObservableCollection<Category>(BudgetManager.GetCategories());
        }
        private void Delete(BudgetRow selectedBudgetRow)
        {
            if (SelectedBudgetRow == null)
            {
                MessageBox.Show("Please select budget row to delete!", "No budget row selected!");
                return;
            }
            BudgetManager.BudgetRows.Remove(selectedBudgetRow);
            BudgetRows = new ObservableCollection<BudgetRow>(BudgetManager.BudgetRows);
        }
        
    }    
}
