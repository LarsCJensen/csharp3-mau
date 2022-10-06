using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL.Services
{
    public abstract class BaseService<T>
    {
        public abstract Dictionary<string, string> Save(T entity);
        public abstract bool Delete(int id);
        public abstract T GetById(int id);
        public abstract IEnumerable<T> GetItems();
        protected abstract bool Validate(T entity);
    }
}
