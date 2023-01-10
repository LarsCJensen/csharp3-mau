using LoveYourBudget.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.BLL.Services
{
    /// <summary>
    /// Service for Category
    /// </summary>
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
        /// <summary>
        /// Implementation of GetItems
        /// </summary>
        /// <returns>IEnumerable of budget</returns>
        public override IEnumerable<Category> GetItems()
        {
            RecreateContext();
            return _repository.GetEntitiesNoTracking();
        }
        /// <summary>
        /// NOT IMPLEMENTED
        /// Method to get Category by id
        /// </summary>
        /// <param name="id">id of Category</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Category GetById(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// NOT IMPLEMENTED
        /// Method to delete Category
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
