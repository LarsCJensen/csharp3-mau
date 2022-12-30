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
        private ObservableCollection<Category> _categories= new ObservableCollection<Category>();
        public ObservableCollection<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
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
        #region EventHandlers
        public event EventHandler OnSave;
        #endregion
        #region Commands
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand<BudgetRow> DeleteCommand { get; private set; }
        #endregion
        public NewBudgetViewModel() 
        {
            _budgetManager= new BudgetManager();
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
            _budgetManager.Save();
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
                //Category = SelectedCategory,
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
            BudgetManager.BudgetRows.Remove(selectedBudgetRow);
            BudgetRows = new ObservableCollection<BudgetRow>(BudgetManager.BudgetRows);
        }
        
    }    
}
