using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.DAL.Models;

namespace Assignment2.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly MediaPlayerDbContext _context;
        public Repository(MediaPlayerDbContext context)
        {
            _context = context;
        }
        public T Save(T entity)
        {
            if(entity.id == 0)
            {
                return SaveNew(entity);
            }
            // TODO Var ok?
            var dbItem = GetById(entity.id);
            _context.Entry(dbItem).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entity;
        }
        private T SaveNew(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var dbItem = GetById(id);
            if (dbItem == null) {
                // TODO Capture
                throw new InvalidOperationException("Item not found in database!");
            }
            _context.Set<T>().Remove(dbItem);
            _context.SaveChanges();
            // Get entity
            // If not found
            // ThrowError
            // Remove
            // Save changes
        }

        public T GetById(int id)
        {
            return GetEntities().FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<T> GetEntities()
        {
            return _context.Set<T>();
        }
    }
}
