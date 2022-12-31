using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.BLL.Services
{
    public abstract class BaseService<T>
    {
        public abstract void Save(T entity);
        public abstract void RecreateContext();
        public abstract IEnumerable<T> GetItems();
        public abstract T GetById(int id);
        public abstract void Delete(int id);
    }
}
