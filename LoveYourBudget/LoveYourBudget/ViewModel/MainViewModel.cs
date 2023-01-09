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
    /// <summary>
    /// ViewModel for MainWindow
    /// </summary>
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
        // Static list with years to choose from
        public List<string> Years => new List<string>()
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
        // Static list with months to choose from
        public List<string> Months => new List<string>()
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
        private bool _deleteEnabled;
        public bool DeleteEnabled
        {
            get
            {
                return _deleteEnabled;
            }
            set
            {
                _deleteEnabled = value;
                OnPropertyChanged("DeleteEnabled");
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
        private bool _noBudget;
        public bool NoBudget
        {
            get { return _noBudget; }
            set { 
                _noBudget = value;
                OnPropertyChanged("NoBudget");                    
            }
        }

        #endregion
        #region Commands
        public RelayCommand YearChangedCommand { get; private set; }
        public RelayCommand MonthChangedCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand DeleteBudgetCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand CreateTestDataCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        #endregion
        public MainViewModel()
        {
            SelectedYear = "2023";
            SelectedMonth = "01";
            BudgetManager = new BudgetManager(SelectedYear, SelectedMonth);
            RefreshGUI();

        }
        /// <summary>
        /// Helper method to refresh GUI
        /// </summary>
        private void RefreshGUI()
        {
            LoadCategoriesAsync();
            LoadExpenses();
            Date = DateTime.Now;
            BudgetExpenses = BudgetManager.GetSumBudgetExpenses();
            Income = BudgetManager.GetSumIncome();            
            // FUTURE Not fully implemented
            TopExpenseCategory = BudgetManager.GetTopExpenseCategory(SelectedYear, SelectedMonth);            
            NumberOfBudgets = $"# Budgets: {BudgetManager.Budgets.Count}";
            NumberOfBudgetRows = $"# Budget rows: {BudgetManager.BudgetRows.Count}";
            EditOrCreateBudget = BudgetManager.Budgets.Count > 0 ? "Edit budget" : "Create budget";
            EditEnabled = SelectedMonth != "" && (BudgetManager.Budgets.Count == 1 || EditOrCreateBudget == "Create budget") ? true : false;
            DeleteEnabled = SelectedMonth != "" && BudgetManager.Budgets.Count == 1 ? true : false;
            NoBudget = BudgetManager.Budgets.Count == 0 ? true: false;
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            YearChangedCommand = new RelayCommand(YearChangedExecute);
            MonthChangedCommand = new RelayCommand(MonthChangedExecute);
            AddCommand = new RelayCommand(AddExecute);
            DeleteBudgetCommand = new RelayCommand(DeleteBudgetExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            CreateTestDataCommand = new RelayCommand(CreateTestData);
            CloseCommand = new RelayCommand(Close);
        }
        /// <summary>
        /// Handler for OnSave event. Used to reload budget view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSave(object sender, EventArgs e)
        {
            MessageBox.Show("Item saved!", "Save completed!", MessageBoxButton.OK, MessageBoxImage.Information);
            BudgetManager = new BudgetManager(SelectedYear, SelectedMonth);
            RefreshGUI();
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
        private void AddExecute()
        {
            // Add through separate thread
            if (SelectedCategory == null)
            {
                MessageBox.Show("Please select category before saving", "Select category!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Double.TryParse(Amount, out double result);
            if (result == 0)
            {
                MessageBox.Show("Please add an amount before saving", "Add amount", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
            RefreshGUI();
        }
        private void DeleteBudgetExecute()
        {            
            try
            {
                BudgetManager.DeleteBudget();
                BudgetManager = new BudgetManager(SelectedYear, SelectedMonth);
                RefreshGUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete budget:\n{ex.InnerException} ");
            }
        }
        private void DeleteExecute()
        {
            if(SelectedExpenseRow == null)
            {
                MessageBox.Show("Please select expense to delete!", "No expense selected!");
                return;
            }
            try
            {
                BudgetManager.DeleteExpense(SelectedExpenseRow.Id);
                RefreshGUI();
            } catch(Exception ex)
            {
                MessageBox.Show($"Could not delete expense: {ex.Message} ");
            }
        }
        private async void LoadCategoriesAsync()
        {
            // Since this is a I/O bound task Task.Run is used
            await Task.Run(() =>
            {

                try
                {
                    Categories = new ObservableCollection<Category>(BudgetManager.GetCategories());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not get categories:\n {ex.InnerException}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            });
        }
        private void LoadExpenses()
        {
            try
            {
                // Since this is a I/O bound task Task.Run is used
                Task getExpensesTask = Task.Run(() =>
                {
                    if (SelectedMonth == "")
                    {
                        ExpenseRows = new ObservableCollection<ExpenseRow>(BudgetManager.GetExpensesForDate(SelectedYear).ToList());
                    } else
                    {
                        ExpenseRows = new ObservableCollection<ExpenseRow>(BudgetManager.GetExpensesForDate(SelectedYear, SelectedMonth).ToList());
                    }
                    
                });
                getExpensesTask.Wait();
                ActualExpenses = ExpenseRows.Sum(x => x.Amount);
            } catch(Exception ex)
            {
                MessageBox.Show($"Could not get expenses: {ex.Message} ");
            }            
        }
        
        private void CreateTestData()
        {
            try
            {
                BudgetManager.CreateTestData();
                MessageBox.Show("Test data created!");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                if(ex.InnerException.Message.Contains("Violation of PRIMARY KEY"))
                {
                    MessageBox.Show("Could not create test data:\n\nOnly one budget allowed per year/month!", "Could not create data!", MessageBoxButton.OK, MessageBoxImage.Error);
                } else
                {
                    MessageBox.Show($"Could not create test data:\n{ex.InnerException}", "Could not create data!", MessageBoxButton.OK, MessageBoxImage.Error);
                }                
            } catch(Exception ex)
            {
                MessageBox.Show($"Unhandled error occured:\n{ex.Message}", "Could not create data!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
