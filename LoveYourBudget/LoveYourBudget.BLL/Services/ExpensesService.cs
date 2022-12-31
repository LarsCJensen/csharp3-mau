using LoveYourBudget.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.BLL.Services
{
    public class ExpensesService : BaseService<ExpenseRow>
    {
        private IRepository<ExpenseRow> _repository;
        public ExpensesService() 
        {
            RecreateContext();
        }
        public override void Save(ExpenseRow entity)
        {
            _repository.Save(entity);
        }
        /// <summary>
        /// Work around to make sure not to get old values based on context cache
        /// </summary>
        public override void RecreateContext()
        {
            LoveYourBudgetDbContext _context = new LoveYourBudgetDbContext();
            _repository = new Repository<ExpenseRow>(_context);
        }
        public override IEnumerable<ExpenseRow> GetItems()
        {
            RecreateContext();
            return _repository.GetEntities();
        }
        /// <summary>
        /// Method to get by ID
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Item</returns>
        public override ExpenseRow GetById(int id)
        {
            RecreateContext();
            return _repository.GetById(id);
        }

        public async Task<IEnumerable<ExpenseRow>> GetExpensesByDateAsync(string year, string month)
        {
            RecreateContext();
            return await _repository.GetExpensesByDateAsync(year, month);
        }
        /// <summary>
        /// Method for delete
        /// </summary>
        /// <param name="expenseId">Expense ID</param>
        public override void Delete(int expenseId)
        {
            RecreateContext();
            _repository.Delete(expenseId);
        }
    }
}
