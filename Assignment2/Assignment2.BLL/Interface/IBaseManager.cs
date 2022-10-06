using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL.Interface
{
    // TODO REMOVE
    public abstract class IBaseManager<T>
    {
        public abstract bool Save();
        public abstract List<T> GetItems();
    }
}
