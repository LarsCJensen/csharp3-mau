using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assignment4B.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Assignment4B.DAL.Repositories
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
        public virtual T GetById(int id)
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
        public IEnumerable<T> SearchEntities(string searchText, string searchProperty, string searchCriteria)
        {
            var query = _context.Set<T>();
            if(searchCriteria == "Contains")
            {
                return AddFilterContains(query, searchText, searchProperty);
            } else
            {
                return AddFilterEquals(query, searchText, searchProperty);
            }
            
            
        }
        /// <summary>
        /// Helper method to add filter dynamically. Only works with string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Query of type T</param>
        /// <param name="propertyName">WHich property to search in</param>
        /// <param name="searchTerm">What text to search for</param>
        /// <returns>Query with filter</returns>
        private IQueryable<T> AddFilterContains<T>(IQueryable<T> query, string searchText, string searchProperty)
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
            
            var filterLambda = Expression.Lambda<Func<T, bool>>(
                call,
                param
            );

            return query.Where(filterLambda);
        }
        private IQueryable<T> AddFilterEquals<T>(IQueryable<T> query, string searchText, string searchProperty)
        {
            // Get type
            var param = Expression.Parameter(typeof(T), "e");
            // Get property form string
            var propExpression = Expression.Property(param, searchProperty);

            object value = searchText;
            if (propExpression.Type != typeof(string))
                value = Convert.ChangeType(value, propExpression.Type);
            var filterLambda = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    propExpression,
                    Expression.Constant(value)
                ),
                param
            );
            
            return query.Where(filterLambda);
        }
    }
}
