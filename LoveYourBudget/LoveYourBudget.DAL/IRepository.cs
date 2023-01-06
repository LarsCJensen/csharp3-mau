﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.DAL
{
    public interface IRepository<T> where T: class
    {
        /// <summary>
        /// Interface for generic repository
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetEntities();
        IEnumerable<T> GetEntitiesNoTracking();
        IEnumerable<ExpenseRow> GetExpensesByDate(string year);
        IEnumerable<ExpenseRow> GetExpensesByDate(string year, string month);
        Task<IEnumerable<ExpenseRow>> GetExpensesByDateAsync(string year);
        Task<IEnumerable<ExpenseRow>> GetExpensesByDateAsync(string year, string month);
        IEnumerable<Budget> GetBudgetsByDate(string year);
        IEnumerable<Budget> GetBudgetsByDate(string year, string month);
        Category GetTopExpenseCategory(string year, string month);
        // TODO REMOVE?
        //IEnumerable<T> SearchEntities(string searchText, string searchProperty, string searchCriteria);
        T GetById(int id);
        T Save(T entity);
        void Delete(int id);
    }
}
