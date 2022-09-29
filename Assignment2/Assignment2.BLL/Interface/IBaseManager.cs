using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL.Interface
{
    public abstract class IBaseManager
    {
        public abstract bool Save<T>(T entity); 
    }
}
