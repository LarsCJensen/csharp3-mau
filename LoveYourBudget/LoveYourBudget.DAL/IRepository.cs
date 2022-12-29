using System;
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
        // TODO REMOVE?
        //IEnumerable<T> SearchEntities(string searchText, string searchProperty, string searchCriteria);
        T GetById(int id);
        T Save(T entity);
        void Delete(int id);
    }
}
