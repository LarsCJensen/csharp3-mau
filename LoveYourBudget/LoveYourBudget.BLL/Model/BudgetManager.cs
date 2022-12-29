using LoveYourBudget.BLL.Services;

namespace LoveYourBudget.BLL.Model
{
    public class BudgetManager
    {
        private BudgetService _budgetService;
        public Budget Budget { get; set; }
        
        public BudgetManager() 
        {
            _budgetService = new BudgetService();
            Budget = new Budget();
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
            _budgetService.Save(Budget);
        }
    }
}
