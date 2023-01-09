using LoveYourBudget.BLL.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LoveYourBudget.BLL.Model
{
    public class BudgetManager
    {
        private BudgetService _budgetService = new BudgetService();
        private CategoryService _categoryService = new CategoryService();
        private ExpensesService _expensesService = new ExpensesService();
        // TODO Is this optimal??
        public Budget Budget { get; set; }
        public List<Budget> Budgets { get; set; }
        public List<BudgetRow> BudgetRows { get; set; }
        
        public BudgetManager() 
        {
            //_budgetService = new BudgetService();
            //_categoryService = new CategoryService();
            //_expensesService = new ExpensesService();
            Budget = new Budget();
            Budgets = new List<Budget>();
            BudgetRows = new List<BudgetRow>();
        }
        public BudgetManager(string year)        
        {
            // Get's all budgets for selected year
            Budgets = _budgetService.GetBudgetsByDate(year).ToList();
            BudgetRows = new List<BudgetRow>();
            foreach(Budget budget in Budgets)
            {
                BudgetRows.AddRange(budget.BudgetRows);
            }
        }
        public BudgetManager(string year, string month)
        {
            // Get's all budgets for selected year and month
            Budgets = _budgetService.GetBudgetsByDate(year, month).ToList();            
            BudgetRows = new List<BudgetRow>();
            if(Budgets.Count > 0)
            {                
                // TODO Even if there are more budgets, only use the first one
                Budget = Budgets.First();
                BudgetRows = Budget.BudgetRows.ToList();
            }            
        }
        
        public double GetSumBudgetExpenses()
        {
            double budgetSum = 0;
            // Sum expenses for Budget.Id
            foreach(Budget budget in Budgets)
            {
                foreach(BudgetRow row in budget.BudgetRows)
                {
                    budgetSum += row.Amount;
                }                
            }
            return budgetSum;
        }
        public double GetSumIncome()
        {
            double budgetIncome = 0;
            // Sum expenses for Budget.Id
            foreach (Budget budget in Budgets)
            {
                budgetIncome += budget.Income;        
            }
            return budgetIncome;
        }
        /// <summary>
        /// NOT YET IMPLEMENTED!
        /// Get top expense category for date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetTopExpenseCategory(string year, string month)
        {
            // Sum expenses for Category for budgets
            //_expensesService.GetTopExpenseCategory(year, month);
            return "Groceries";
        }
        public void SaveBudget()
        {
            if(Budget.CreatedTime == null)
            {
                Budget.CreatedTime = DateTime.Now;
            }
            // TODO Ìs there a better way?
            Budget.BudgetRows = BudgetRows;
            _budgetService.Save(Budget);
        }
        public void DeleteBudget()
        {
            //foreach(BudgetRow budgetRow in Budget.BudgetRows)
            //{
            //    //_budgetService.Delete();
            //}
                
            _budgetService.Delete(Budget.Id);
        }
        public void SaveExpense(ExpenseRow expenseRow)
        {
            _expensesService.Save(expenseRow);
        }
        public List<Category> GetCategories()
        {
            return _categoryService.GetItems().ToList();
        }
        /// <summary>
        /// Method to get expenses by year
        /// </summary>
        /// <param name="year">Year</param>
        //4/ <returns></returns>
        public IEnumerable<ExpenseRow> GetExpensesForDate(string year)
        {
            return _expensesService.GetExpensesByDate(year);
        }
        /// <summary>
        /// Method to get expenses by year and month
        /// </summary>
        /// <param name="year">Year</param>
        /// /// <param name="month">Month</param>
        /// <returns></returns>
        public IEnumerable<ExpenseRow> GetExpensesForDate(string year, string month)
        {
            return _expensesService.GetExpensesByDate(year, month);
        }
        /// <summary>
        /// Method to get expenses by year and month
        /// </summary>
        /// <param name="year">Year</param>
        /// /// <param name="month">Month</param>
        /// <returns></returns>
        public IEnumerable<ExpenseRow> GetExpensesByYearAndCategory(string year, int categoryId)
        {
            return _expensesService.GetExpensesByYearAndCategory(year, categoryId);
        }
        /// <summary>
        /// Method to get expenses by year async
        /// </summary>
        /// <param name="year">Year</param>
        /// <returns></returns>
        public async Task<IEnumerable<ExpenseRow>> AsyncGetExpenses(string year)
        {
            return await _expensesService.AsyncGetExpensesByDate(year);
        }
        /// <summary>
        /// Method to get expenses by year async
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns></returns>
        public async Task<IEnumerable<ExpenseRow>> AsyncGetExpenses(string year, string month)
        {
            return await _expensesService.AsyncGetExpensesByDate(year, month);
        }
        public void DeleteExpense(int expenseId)
        {
            _expensesService.Delete(expenseId);
        }
        #region CreateTestData
        /// <summary>
        /// Only a helper method to create test data
        /// </summary>
        public void CreateTestData()
        {
            Random random = new Random();
            // Creates budgets for 2022 
            for (int i = 1; i < 13; i++)
            {
                string month = "0";
                if(i < 10)
                {
                    month += i.ToString();
                } else
                {
                    month = i.ToString();
                }
                Budget = new Budget()
                {
                    Year = "2022",
                    Month = month,
                    Income = random.Next(20000, 60000),
                };

                CreateBudgetRows(2022, i);
                CreateExpenseRows(2022, i);
                SaveBudget();                
            }
        }
        private void CreateBudgetRows(int year, int month)
        {
            Random random = new Random();
            BudgetRows = new List<BudgetRow>();
            for (int i = 1; i < _categoryService.GetItems().Count(); i ++)
            {
                BudgetRows.Add(
                        new BudgetRow()
                        {
                            CreatedTime= DateTime.Now,
                            CategoryId = i,
                            Amount = random.Next(random.Next(1000, 5000)),
                            Date = new DateTime(year, month, 02),
                        }
                );
            }                
        }
        private void CreateExpenseRows(int year, int month)
        {
            Random random = new Random();            
            for (int i = 1; i < _categoryService.GetItems().Count()+1; i++)
            {
                ExpenseRow expense = new ExpenseRow()
                    {
                        CreatedTime = DateTime.Now,
                        CategoryId = i,
                        Amount = random.Next(random.Next(1000, 5000)),
                        Date = new DateTime(year, month, 15),
                    };
                SaveExpense(expense);
            }
        }
        #endregion
    }
}
