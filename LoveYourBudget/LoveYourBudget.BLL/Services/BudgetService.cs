using LoveYourBudget.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.BLL.Services
{
    public class BudgetService : BaseService<Budget>
    {
        private IRepository<Budget> _repository;
        public BudgetService() 
        {
            RecreateContext();
        }
        public override void Save(Budget entity)
        {
            _repository.Save(entity);
        }
        /// <summary>
        /// Work around to make sure not to get old values based on context cache
        /// </summary>
        public override void RecreateContext()
        {
            LoveYourBudgetDbContext _context = new LoveYourBudgetDbContext();
            _repository = new Repository<Budget>(_context);
        }
        public override IEnumerable<Budget> GetItems()
        {
            RecreateContext();
            return _repository.GetEntities();
        }
        /// <summary>
        /// Method to get by ID
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Item</returns>
        public override Budget GetById(int id)
        {
            RecreateContext();
            return _repository.GetById(id);
        }
        /// <summary>
        /// Method for delete
        /// </summary>
        /// <param name="budgetId">Budget ID</param>
        public override void Delete(int budgetId)
        {
            RecreateContext();
            _repository.Delete(budgetId);
        }
        /// <summary>
        /// Helper method to get budgets by date
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Budget> GetBudgetsByDate(string year)
        {
            RecreateContext();
            return _repository.GetBudgetsByDate(year);
        }
        /// <summary>
        /// Helper method to get budgets by date
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Budget> GetBudgetsByDate(string year, string month)
        {
            RecreateContext();
            return _repository.GetBudgetsByDate(year, month);
        }
    }
}
