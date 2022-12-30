using LoveYourBudget.BLL.Services;
using System.Collections.ObjectModel;

namespace LoveYourBudget.BLL.Model
{
    public class BudgetManager
    {
        private BudgetService _budgetService;
        private CategoryService _categoryService;
        public Budget Budget { get; set; }
        public List<BudgetRow> BudgetRows { get; set; }
        public List<ExpenseRow> ExpenseRows { get; set; }

        public BudgetManager() 
        {
            _budgetService = new BudgetService();
            _categoryService = new CategoryService();
            Budget = new Budget();
            BudgetRows = new List<BudgetRow>();
            ExpenseRows = new List<ExpenseRow>();
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
        public void Save()
        {
            if(Budget.CreatedTime == null)
            {
                Budget.CreatedTime = DateTime.Now;
            }
            // TODO Ìs there a better way?
            Budget.BudgetRows = BudgetRows;
            _budgetService.Save(Budget);
        }
        public List<Category> GetCategories()
        {
            return _categoryService.GetItems().ToList();
        }
    }
}
