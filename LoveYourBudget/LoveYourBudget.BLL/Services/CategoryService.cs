using LoveYourBudget.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.BLL.Services
{
    public class CategoryService : BaseService<Category>
    {
        private IRepository<Category> _repository;
        public CategoryService() 
        {
            RecreateContext();
        }
        public override void Save(Category entity)
        {
            _repository.Save(entity);
        }
        /// <summary>
        /// Work around to make sure not to get old values based on context cache
        /// </summary>
        public override void RecreateContext()
        {
            LoveYourBudgetDbContext _context = new LoveYourBudgetDbContext();
            _repository = new Repository<Category>(_context);
        }        
        public override IEnumerable<Category> GetItems()
        {
            RecreateContext();
            return _repository.GetEntitiesNoTracking();
        }

        public override Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
