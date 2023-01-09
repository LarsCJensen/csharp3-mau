using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.DAL
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        /// <summary>
        /// Generic repository for all DB calls
        /// </summary>
        private readonly LoveYourBudgetDbContext _context;
        public Repository(LoveYourBudgetDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to save T
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <returns>Saved entity</returns>
        public T Save(T entity)
        {
            // If entity doesn't have id then it is a new entity
            if (entity.Id == 0)
            {
                return SaveNew(entity);
            }
            // If it has id then fetch it from database and update values
            var dbItem = GetById(entity.Id);
            if (dbItem == null)
            {
                throw new InvalidOperationException("Item not found in database!");
            }
            // This workaround did not work
            //_context.ChangeTracker.TrackGraph(entity, e => {
            //    //e.Entry.State = EntityState.Unchanged; //starts tracking
            //    if ((e.Entry.Entity as Album) != null)
            //    {
            //        _context.Entry(e.Entry.Entity as Album).Collection("Files").IsModified = true;
            //    }
            //});

            // For unknown reason, files doesn't get tracked as changed. Using this loop to set all properties before save
            foreach (var item in entity.GetType().GetProperties())
            {
                dbItem.GetType().GetProperty(item.Name).SetValue(dbItem, item.GetValue(entity));
            }
            // This couldn't be used as relationship changes was not tracked                
            //_context.Entry(dbItem).CurrentValues.SetValues(entity);            
            _context.SaveChanges();
            return entity;
        }
        /// <summary>
        /// Helper method to save a new entity
        /// Is virtual to be able to be testable
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <returns>Entity</returns>
        public virtual T SaveNew(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        /// <summary>
        /// Method for delete
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Delete(int id)
        {
            var dbItem = GetById(id);
            if (dbItem == null)
            {
                throw new InvalidOperationException("Item not found in database!");
            }
            _context.Set<T>().Remove(dbItem);
            _context.SaveChanges();
        }
        /// <summary>
        /// Method to get an entity
        /// </summary>
        /// <param name="id">Id of entity to get</param>
        /// <returns>Entity</returns>
        public virtual T GetById(int id)
        {
            return GetEntities().FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// Method to get all entities of a certain type
        /// </summary>
        /// <returns>Entities</returns>
        public IEnumerable<T> GetEntities()
        {
            return _context.Set<T>();
        }
        /// <summary>
        /// Method to get all entities of a certain type without tracking changes
        /// </summary>
        /// <returns>Entities</returns>
        public IEnumerable<T> GetEntitiesNoTracking()
        {
            return _context.Set<T>().AsNoTracking();
        }
        /// <summary>
        /// Method to get all entities of a certain type by date
        /// </summary>
        /// <returns>Entities</returns>
        public IEnumerable<Budget> GetBudgetsByDate(string year)
        {
            return _context.Budgets.AsQueryable().Where(x => x.Year == year).ToList();
        
        }
        /// <summary>
        /// Method to get all entities of a certain type by date
        /// </summary>
        /// <returns>Entities</returns>
        public IEnumerable<Budget> GetBudgetsByDate(string year, string month)
        {
            return _context.Budgets.AsQueryable().Where(x => x.Year == year && x.Month == month).ToList();            
        }
        public Category GetTopExpenseCategory(string year, string month)
        {
            DateTime date = DateTime.Parse(year + "-" + month);
            int daysInMonth = DateTime.DaysInMonth(Int32.Parse(year), Int32.Parse(month));
            DateTime enddate = DateTime.Parse(year + "-" + month + "-" + daysInMonth);
            var expenseRows = GetAllExpenses().Where(x => x.Date >= date && x.Date <= enddate).GroupBy(x => x.CategoryId).ToList();
            
            //var category = _context.Categories.GroupBy(c => c.Name)
            //    .Select(g => new
            //    {
            //        g.Key, SUM = g.Sum(s => s.ExpenseRows.Select(t => t.Amount).Sum())
            //    }).FirstOrDefault();
            //List<BudgetRow> budgetRows = new List<BudgetRow>();
            //if (month == "")
            //{
            //    //budgetRows = GetAllBudgetRows().Where(x => x.Date >= date && x.Date <= enddate).GroupBy(Category);
            //}
            //else
            //{
            //    //return _context.Budgets.AsQueryable().Where(x => x.Year == year && x.Month == month).First();
            //}
            return _context.Categories.First();
        }
        private IQueryable<BudgetRow> GetAllBudgetRows()
        {
            return _context.BudgetRows.AsQueryable();
        }

        /// <summary>
        /// Method to get all ExpenseRows for year
        /// </summary>
        /// <param name="year">year to filter on</param>
        /// /// <param name="month">month to filter on</param>
        /// <returns></returns>
        public IEnumerable<ExpenseRow> GetExpensesByDate(string year, string month)
        {
            DateTime date = DateTime.Parse(year + "-" + month);
            int daysInMonth = DateTime.DaysInMonth(Int32.Parse(year), Int32.Parse(month));
            DateTime enddate = DateTime.Parse(year + "-" + month + "-" + daysInMonth);

            return GetAllExpenses().Where(x => x.Date >= date && x.Date <= enddate).ToList();
        }
        /// <summary>
        /// Method to get all ExpenseRows for year and month
        /// </summary>
        /// <param name="year">year to filter on</param>
        /// <returns></returns>
        public IEnumerable<ExpenseRow> GetExpensesByDate(string year)
        {
            DateTime date = new DateTime(int.Parse(year), 1, 1);
            DateTime enddate = new DateTime(int.Parse(year), 12, 31);
            return GetAllExpenses().Where(x => x.Date >= date && x.Date <= enddate).ToList();
        }
        /// <summary>
        /// Method to get all ExpenseRows for year and month
        /// </summary>
        /// <param name="year">year to filter on</param>
        /// <returns></returns>
        public IEnumerable<ExpenseRow> GetExpensesByYearAndCategory(string year, int categoryId)
        {
            DateTime date = new DateTime(int.Parse(year), 1, 1);
            DateTime enddate = new DateTime(int.Parse(year), 12, 31);
            return GetAllExpenses().Where(x => (x.Date >= date && x.Date <= enddate) && x.CategoryId == categoryId).ToList();
        }
        /// <summary>
        /// Method to get all ExpenseRows by year and month asynchronously
        /// </summary>
        /// <param name="year">year to filter on</param>
        /// <returns></returns>
        public async Task<IEnumerable<ExpenseRow>> AsyncGetExpensesByDate(string year)
        {
            DateTime date = new DateTime(int.Parse(year), 1, 1);
            DateTime enddate = new DateTime(int.Parse(year), 12, 31);
            return await GetAllExpenses().Where(x => x.Date >= date && x.Date <= enddate).ToListAsync();
        }
        /// <summary>
        /// Method to get all ExpenseRows asynchronously
        /// </summary>
        /// <param name="year">year to filter on</param>
        /// <param name="month">month to filter on</param>
        /// <returns></returns>
        public async Task<IEnumerable<ExpenseRow>> AsyncGetExpensesByDate(string year, string month)
        {
            // If month is selected calculate date and enddate
            DateTime date = DateTime.Parse(year + "-" + month);
            int daysInMonth = DateTime.DaysInMonth(Int32.Parse(year), Int32.Parse(month));
            DateTime enddate = DateTime.Parse(year + "-" + month + "-" + daysInMonth);             
            
            return await GetAllExpenses().Where(x => x.Date >= date && x.Date <= enddate).ToListAsync();
            //return await _context.ExpenseRows.Where(x => x.Date > date && x.Date <= enddate).ToListAsync();

        }
        private IQueryable<ExpenseRow> GetAllExpenses()
        {
            return _context.ExpenseRows.AsQueryable();
        }
        // TODO REMOVE
        ///// <summary>
        ///// Method to search entities based on text an property
        ///// </summary>
        ///// <param name="searchText">Text to search for</param>
        ///// <param name="searchProperty">Property to search in</param>
        ///// <returns></returns>
        //public IEnumerable<T> SearchEntities(string searchText, string searchProperty, string searchCriteria)
        //{
        //    var query = _context.Set<T>();
        //    if (searchCriteria == "Contains")
        //    {
        //        return AddFilterContains(query, searchText, searchProperty);
        //    }
        //    else
        //    {
        //        return AddFilterEquals(query, searchText, searchProperty);
        //    }


        //}
        ///// <summary>
        ///// Helper method to add filter dynamically. Only works with string
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="query">Query of type T</param>
        ///// <param name="propertyName">WHich property to search in</param>
        ///// <param name="searchTerm">What text to search for</param>
        ///// <returns>Query with filter</returns>
        //private IQueryable<T> AddFilterContains<T>(IQueryable<T> query, string searchText, string searchProperty)
        //{
        //    // Get type
        //    var param = Expression.Parameter(typeof(T), "e");
        //    // Get property form string
        //    var propExpression = Expression.Property(param, searchProperty);

        //    object value = searchText;
        //    if (propExpression.Type != typeof(string))
        //        value = Convert.ChangeType(value, propExpression.Type);
        //    // Get the Contains method instead of using Equal
        //    var method = typeof(string).GetMethod("Contains", new Type[] { typeof(string) }); ;
        //    var call = Expression.Call(propExpression, method, Expression.Constant(value));

        //    var filterLambda = Expression.Lambda<Func<T, bool>>(
        //        call,
        //        param
        //    );

        //    return query.Where(filterLambda);
        //}
        //private IQueryable<T> AddFilterEquals<T>(IQueryable<T> query, string searchText, string searchProperty)
        //{
        //    // Get type
        //    var param = Expression.Parameter(typeof(T), "e");
        //    // Get property form string
        //    var propExpression = Expression.Property(param, searchProperty);

        //    object value = searchText;
        //    if (propExpression.Type != typeof(string))
        //        value = Convert.ChangeType(value, propExpression.Type);
        //    var filterLambda = Expression.Lambda<Func<T, bool>>(
        //        Expression.Equal(
        //            propExpression,
        //            Expression.Constant(value)
        //        ),
        //        param
        //    );

        //    return query.Where(filterLambda);
        //}
    }
}
