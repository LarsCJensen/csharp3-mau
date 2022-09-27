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
        IEnumerable<T> GetEntities();
        T GetById(int id);
        T Save(T entity);
        void Delete(int id);
    }
}

