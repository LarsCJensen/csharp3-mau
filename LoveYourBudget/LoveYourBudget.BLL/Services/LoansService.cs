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
    /// Service for Loan
    /// </summary>
    public class LoansService : BaseService<Loan>
    {
        private IRepository<Loan> _repository;
        public LoansService() 
        {
            RecreateContext();
        }
        /// <summary>
        /// Work around to make sure not to get old values based on context cache
        /// </summary>
        public override void RecreateContext()
        {
            LoveYourBudgetDbContext _context = new LoveYourBudgetDbContext();
            _repository = new Repository<Loan>(_context);
        }
        /// <summary>
        /// Method to save
        /// </summary>
        /// <param name="entity">Entity to save</param>
        public override void Save(Loan entity)
        {
            _repository.Save(entity);
        }
        /// <summary>
        /// Implementation of GetItems
        /// </summary>
        /// <returns>IEnumerable of loan</returns>
        public override IEnumerable<Loan> GetItems()
        {
            RecreateContext();
            return _repository.GetEntities();
        }
        /// <summary>
        /// Method to get by ID
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Item</returns>
        public override Loan GetById(int id)
        {
            RecreateContext();
            return _repository.GetById(id);
        }        
        /// <summary>
        /// Method for delete
        /// </summary>
        /// <param name="expenseId">Expense ID</param>
        public override void Delete(int loanId)
        {
            RecreateContext();
            _repository.Delete(loanId);
        }
    }
}
