using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assignment2.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Assignment2.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        /// <summary>
        /// Generic repository for all DB calls
        /// </summary>
        private readonly MediaPlayerDbContext _context;
        public Repository(MediaPlayerDbContext context)
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
            if(entity.id == 0)
            {
                return SaveNew(entity);
            }
            // If it has id then fetch it from database and update values
            var dbItem = GetById(entity.id);
            _context.Entry(dbItem).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entity;
        }
        /// <summary>
        /// Helper method to save a new entity
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <returns>Entity</returns>
        private T SaveNew(T entity)
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
            if (dbItem == null) {
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
        public T GetById(int id)
        {
            return GetEntities().FirstOrDefault(x => x.id == id);
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
        /// Method to search entities based on text an property
        /// </summary>
        /// <param name="searchText">Text to search for</param>
        /// <param name="searchProperty">Property to search in</param>
        /// <returns></returns>
        public IEnumerable<T> SearchEntities(string searchText, string searchProperty)
        {
            var query = _context.Set<T>();
            return AddFilter(query, searchProperty, searchText);            
        }
        /// <summary>
        /// Helper method to add filter dynamically. Only works with string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Query of type T</param>
        /// <param name="propertyName">WHich property to search in</param>
        /// <param name="searchTerm">What text to search for</param>
        /// <returns>Query with filter</returns>
        private IQueryable<T> AddFilter<T>(IQueryable<T> query, string searchText, string searchProperty)
        {
            // Get type
            var param = Expression.Parameter(typeof(T), "e");
            // Get property form string
            var propExpression = Expression.Property(param, searchProperty);

            object value = searchText;
            if (propExpression.Type != typeof(string))
                value = Convert.ChangeType(value, propExpression.Type);        
            // Get the Contains method instead of using Equal
            var method = typeof(string).GetMethod("Contains", new Type[] { typeof(string) }); ;
            var call = Expression.Call(propExpression, method, Expression.Constant(value));
            // TODO REMOVE
            //var filterLambda = Expression.Lambda<Func<T, bool>>(
            //    Expression.Equal(
            //        propExpression,
            //        Expression.Constant(value)
            //    ),
            //    param
            //);
            var filterLambda = Expression.Lambda<Func<T, bool>>(
                call,
                param
            );

            return query.Where(filterLambda);
        }
    }
}
