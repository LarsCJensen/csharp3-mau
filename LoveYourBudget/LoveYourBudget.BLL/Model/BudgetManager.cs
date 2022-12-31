using LoveYourBudget.BLL.Services;
using System.Collections.ObjectModel;

namespace LoveYourBudget.BLL.Model
{
    public class BudgetManager
    {
        private BudgetService _budgetService;
        private CategoryService _categoryService;
        private ExpensesService _expensesService;
        public Budget Budget { get; set; }
        public List<BudgetRow> BudgetRows { get; set; }
        
        public BudgetManager() 
        {
            _budgetService = new BudgetService();
            _categoryService = new CategoryService();
            _expensesService = new ExpensesService();
            Budget = new Budget();
            BudgetRows = new List<BudgetRow>();
        }
        public int GetSumBudgetExpenses()
        {
            // Sum expenses for Budget.Id
            return 0;
        }
        public int GetSumExpenses()
        {
            // Sum expenses for Budget.Id
            return 0;
        }
        public string GetTopExpenseCategory()
        {
            // Sum expenses for Category with Budget.Id
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
            // TODO Get by Year, month
            return await _expensesService.GetExpensesByDateAsync(year, month);
        }
        public void DeleteExpense(int expenseId)
        {
            _expensesService.Delete(expenseId);
        }
    }
}
