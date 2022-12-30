using GalaSoft.MvvmLight.Command;
using LoveYourBudget.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoveYourBudget.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BudgetManager _budgetManager;
        public BudgetManager BudgetManager
        {
            get { return _budgetManager; }
            set
            {
                _budgetManager = value;
                OnPropertyChanged("BudgetManager");
            }
        }
        private string _title = "Love Your Budget";
        public string Title
        {
            get
            {
                return _title;
            }
        }
        public List<String> Years => new List<String>()
                {
                    "2023", "2022"
                };
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
        public List<String> Months => new List<String>()
                {
                    "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                };
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
        public int Income
        {
            get { return _budgetManager.Budget.Income; }
        }
        public int BudgetExpenses
        {
            get { return _budgetManager.GetSumBudgetExpenses(); }
        }
        public int ActualExpenses
        {
            get { return _budgetManager.GetSumExpenses(); }
        }
        public string TopExpenseCategory
        {
            get { return _budgetManager.GetTopExpenseCategory(); }
        }
        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
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
        private ObservableCollection<ExpenseRow> _expenseRows = new ObservableCollection<ExpenseRow>();
        public ObservableCollection<ExpenseRow> ExpenseRows
        {
            get
            {
                return _expenseRows;
            }
            set
            {
                _expenseRows = value;
                OnPropertyChanged("ExpenseRows");
            }
        }
        private ExpenseRow _selectedExpenseRow;
        public ExpenseRow SelectedExpenseRow
        {
            get
            {
                return _selectedExpenseRow;
            }
            set
            {
                _selectedExpenseRow = value;
                OnPropertyChanged("SelectedExpenseRow");
            }
        }
        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        #region Commands
        public RelayCommand YearChangedCommand { get; private set; }
        public RelayCommand MonthChangedCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        #endregion
        public MainViewModel()
        {
            _budgetManager = new BudgetManager();
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            YearChangedCommand = new RelayCommand(YearChangedExecute);
            MonthChangedCommand = new RelayCommand(MonthChangedExecute);
            AddCommand = new RelayCommand(Add);
        }
        /// <summary>
        /// Handler for OnSave event. Used to reload budget view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSave(object sender, EventArgs e)
        {
            MessageBox.Show("Saved!");
        }
        private void YearChangedExecute()
        {
            // TODO Get data in separate thread
            MessageBox.Show("test");
        }
        private void MonthChangedExecute()
        {
            // TODO Get data in separate thread
            MessageBox.Show("test");
        }
        private void Add()
        {
            // Add through separate thread
            // TODO Validate
            Double.TryParse(Amount, out double result);
            ExpenseRow ExpenseRow = new ExpenseRow()
            {
                CreatedTime = DateTime.Now,
                CategoryId = SelectedCategory.Id,
                Amount = result,
                Date = Date,
            };
            BudgetManager.ExpenseRows.Add(ExpenseRow);
            BudgetManager.Save();
            SelectedCategory = null;
            Amount = "";
            ExpenseRows = new ObservableCollection<ExpenseRow>(BudgetManager.ExpenseRows);
        }
    }
}
