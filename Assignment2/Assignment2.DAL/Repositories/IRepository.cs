using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Repositories
{
    public interface IRepository<T> where T:class
    {
        public static IEnumerable<T> ListItems;

    }
}
