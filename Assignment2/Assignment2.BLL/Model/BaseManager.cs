using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.BLL.Interface;
using Assignment2.DAL;
using Assignment2.DAL.Models;
using Assignment2.DAL.Repositories;
using Assignment2.Utilities;
using FileBase = Assignment2.DAL.Models.FileBase;

namespace Assignment2.BLL
{
    public abstract class BaseManager<T>
    {        
        public abstract Dictionary<string, string> Save();
        public abstract bool Delete(int id);
        public abstract List<T> GetItems();
        /// <summary>
        /// Method to get count of each file extension
        /// </summary>
        /// <param name="filesExtensions">List of extensions</param>
        /// <param name="extensions">List of extensions to compare against</param>
        /// <returns></returns>
        public int GetCount(List<string> filesExtensions, List<string> extensions)
        {
            var extensionsCount = filesExtensions.Count(f => extensions.Contains(f));
            return extensionsCount;
        }
        public abstract List<T> SearchItems(string searchText, string searchProperty, string searchCriteria);
    }
}
