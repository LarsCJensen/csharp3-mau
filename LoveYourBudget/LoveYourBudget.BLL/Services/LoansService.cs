using LoveYourBudget.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.BLL.Services
{
    public class LoansService : BaseService<Loan>
    {
        private IRepository<Loan> _repository;
        public LoansService() 
        {
            RecreateContext();
        }
        public override void Save(Loan entity)
        {
            _repository.Save(entity);
        }
        /// <summary>
        /// Work around to make sure not to get old values based on context cache
        /// </summary>
        public override void RecreateContext()
        {
            LoveYourBudgetDbContext _context = new LoveYourBudgetDbContext();
            _repository = new Repository<Loan>(_context);
        }
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
        // TODO REMOVE
        ///// <summary>
        ///// Method to get expenses per Year
        ///// </summary>
        ///// <param name="year"></param>
        ///// <returns></returns>
        //public IEnumerable<Loan> GetExpensesByDate(string year)
        //{
        //    return _repository.GetExpensesByDate(year);
        //}
        ///// <summary>
        ///// Method to get expenses per Year
        ///// </summary>
        ///// <param name="year"></param>
        ///// <returns></returns>
        //public IEnumerable<ExpenseRow> GetExpensesByDate(string year, string month)
        //{
        //    return _repository.GetExpensesByDate(year, month);
        //}
        ///// <summary>
        ///// Method to get expenses by year async
        ///// </summary>
        ///// <param name="year">Year</param>        
        ///// <returns>Task</returns>
        //public async Task<IEnumerable<ExpenseRow>> GetExpensesByDateAsync(string year)
        //{
        //    return await _repository.GetExpensesByDateAsync(year);
        //}
        //public async Task<IEnumerable<ExpenseRow>> GetExpensesByDateAsync(string year, string month)
        //{
        //    RecreateContext();
        //    return await _repository.GetExpensesByDateAsync(year, month);
        //}
        //public Category GetTopExpenseCategory(string year, string month)
        //{
        //    return _repository.GetTopExpenseCategory(year, month);
        //}
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
