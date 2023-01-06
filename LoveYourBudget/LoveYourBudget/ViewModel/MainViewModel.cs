using GalaSoft.MvvmLight.Command;
using LoveYourBudget.BLL.Model;
using Microsoft.EntityFrameworkCore;
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
        #region Properties
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
        public string _numberOfBudgets;
        public string NumberOfBudgets
        {
            get { 
                return _numberOfBudgets; 
            }
            set
            {
                _numberOfBudgets = value;
                OnPropertyChanged("NumberOfBudgets");
            }
        }
        public string _numberOfBudgetRows;
        public string NumberOfBudgetRows
        {
            get
            {
                return _numberOfBudgetRows;
            }
            set
            {
                _numberOfBudgetRows = value;
                OnPropertyChanged("NumberOfBudgetRows");
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
                    "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"
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
        private double _income;
        public double Income
        {
            get 
            {
                return _income;
            }
            set
            {
                _income = value;
                OnPropertyChanged("Income");
            }
        }
        private double _budgetExpenses;
        public double BudgetExpenses
        {
            get { 
                return _budgetExpenses; 
            }
            set
            {
                _budgetExpenses = value;
                OnPropertyChanged("BudgetExpenses");
            }
        }
        private double _actualExpenses;
        public double ActualExpenses
        {
            get 
            { 
                return _actualExpenses; 
            } 
            set
            {
                _actualExpenses = value;
                OnPropertyChanged("ActualExpenses");
            }

        }
        private string _topExpenseCategory;
        public string TopExpenseCategory
        {
            get 
            {
                return _topExpenseCategory;                
            }
            set
            {
                _topExpenseCategory = value;
                OnPropertyChanged("TopExpenseCategory");
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
        private bool _editEnabled;
        public bool EditEnabled
        {
            get
            {
                return _editEnabled;
            }
            set
            {
                _editEnabled = value;
                OnPropertyChanged("EditEnabled");
            }
        }
        private string _editOrCreateBudget;
        public string EditOrCreateBudget
        {
            get
            {
                return _editOrCreateBudget;
            }
            set
            {
                _editOrCreateBudget = value;
                OnPropertyChanged("EditOrCreateBudget");
            }
        }
        #endregion
        #region Commands
        public RelayCommand YearChangedCommand { get; private set; }
        public RelayCommand MonthChangedCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand CreateTestDataCommand { get; private set; }
        #endregion
        public MainViewModel()
        {
            SelectedYear = "2023";
            SelectedMonth = "01";
            BudgetManager = new BudgetManager(SelectedYear, SelectedMonth);
            RefreshGUI();

        }
        private void RefreshGUI()
        {
            LoadCategories();
            LoadExpensesAsync();
            Date = DateTime.Now;
            BudgetExpenses = BudgetManager.GetSumBudgetExpenses();
            Income = BudgetManager.GetSumIncome();
            //ActualExpenses = GetSumExpenses();
            TopExpenseCategory = test();
            // TODO Get number of budgets per year instead?
            NumberOfBudgets = $"# Budgets: {BudgetManager.Budgets.Count}";
            NumberOfBudgetRows = $"# Budget rows: {BudgetManager.BudgetRows.Count}";
            EditOrCreateBudget = BudgetManager.Budgets.Count == 1 ? "Edit budget" : "Create budget";
            EditEnabled = SelectedMonth != "" && (BudgetManager.Budgets.Count == 1 || EditOrCreateBudget == "Create budget") ? true : false;
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            YearChangedCommand = new RelayCommand(YearChangedExecute);
            MonthChangedCommand = new RelayCommand(MonthChangedExecute);
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete);
            CreateTestDataCommand = new RelayCommand(CreateTestData);
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
            if(SelectedMonth  == "")
            {
                BudgetManager = new BudgetManager(SelectedYear);
            } else
            {
                BudgetManager = new BudgetManager(SelectedYear, SelectedMonth);
            }
            RefreshGUI();

        }
        private void MonthChangedExecute()
        {
            if (SelectedMonth == "")            
            {
                BudgetManager = new BudgetManager(SelectedYear);
            }
            else
            {
                BudgetManager = new BudgetManager(SelectedYear, SelectedMonth);
            }
                
            RefreshGUI();
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
            BudgetManager.SaveExpense(ExpenseRow);
            SelectedCategory = null;
            Amount = "";
            ExpenseRows.Add(ExpenseRow);
        }
        private void Delete()
        {
            if(SelectedExpenseRow == null)
            {
                MessageBox.Show("Please select expense to delete!", "No expense selected!");
                return;
            }
            try
            {
                BudgetManager.DeleteExpense(SelectedExpenseRow.Id);
                ExpenseRows.Remove(SelectedExpenseRow);
            } catch(Exception ex)
            {
                MessageBox.Show($"Could not delete expense: {ex.Message} ");
            }
        }
        private void LoadCategories()
        {
            try
            {
                Categories = new ObservableCollection<Category>(BudgetManager.GetCategories());
            } catch(Exception ex) 
            {
                MessageBox.Show($"Could not get categories: {ex.Message} ");
            }
        }
        private async void LoadExpensesAsync()
        {
            try
            {
                ExpenseRows = await Task.Run(() => new ObservableCollection<ExpenseRow>(BudgetManager.GetExpensesAsync(SelectedYear, SelectedMonth).Result));
                ActualExpenses = ExpenseRows.Sum(x => x.Amount);
            } catch(Exception ex)
            {
                MessageBox.Show($"Could not get expenses: {ex.Message} ");
            }            
        }
        /// <summary>
        /// Helper method to summarize expenses
        /// </summary>
        /// <returns></returns>
        //private double GetSumExpenses()
        //{
            
        //    double sum = 0;
        //    foreach(ExpenseRow row in ExpenseRows) 
        //    {
        //        sum += row.Amount;
        //    }
        //    //return sum;
        //    // TODO Move to property
        //    return ExpenseRows.Sum(x => x.Amount);
        //}
        private string test()
        {
            // TODO 
            //var category = ExpenseRows.GroupBy(e => e.CategoryId)
            //    .Select(g => new
            //    {
            //        g.Key,
            //        SUM = g.Sum(s => s.Amount)
            //    }).First();
            //MessageBox.Show("test");
            return "Test";
        }
        private void CreateTestData()
        {
            BudgetManager.CreateTestData();
        }
    }
}
