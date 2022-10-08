using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Repositories
{
    public interface IRepository<T> where T:class
    {
        /// <summary>
        /// Interface for generic repository
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetEntities();
        IEnumerable<T> SearchEntities(string searchText, string searchProperty);
        T GetById(int id);
        T Save(T entity);
        void Delete(int id);
    }
}

