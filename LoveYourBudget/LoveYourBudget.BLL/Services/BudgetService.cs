using LoveYourBudget.DAL;
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
    }
}
