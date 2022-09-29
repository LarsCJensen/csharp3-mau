using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL.Services
{
    public abstract class BaseService<T>
    {
        public abstract bool Save(T entity);
        protected abstract bool Validate(T entity);
    }
}
