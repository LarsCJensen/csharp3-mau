using LoveYourBudget.BLL.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.ObjectModel;

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
        public BudgetManager(string year, string month)
        {
            //_budgetService = new BudgetService();
            //_categoryService = new CategoryService();
            //_expensesService = new ExpensesService();
            // Get's all budgets for selected year/month
            Budgets = _budgetService.GetBudgetsByDate(year, month).ToList();
            BudgetRows = new List<BudgetRow>();
            if (Budgets.Count == 1) 
            {
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
        public string GetTopExpenseCategory(string year, string month)
        {
            // Sum expenses for Category for budgets
            _expensesService.GetTopExpenseCategory(year, month);
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
        public void SaveExpense(ExpenseRow expenseRow)
        {
            _expensesService.Save(expenseRow);
        }
        public List<Category> GetCategories()
        {
            return _categoryService.GetItems().ToList();
        }
        public async Task<IEnumerable<ExpenseRow>> GetExpensesAsync(string year, string month)
        {
            return await _expensesService.GetExpensesByDateAsync(year, month);
        }
        public void DeleteExpense(int expenseId)
        {
            _expensesService.Delete(expenseId);
        }
        /// <summary>
        /// Only a helper method to create test data
        /// </summary>
        public void CreateTestData()
        {
            Random random = new Random();
            // Skapa budget för 2022
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
            // Skapa BudgetRows för 2022
            // Skapa expenses för 2022
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
            for (int i = 1; i < _categoryService.GetItems().Count(); i++)
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
    }
}
