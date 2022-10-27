using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4B.BLL.Interface;
using Assignment4B.DAL;
using Assignment4B.DAL.Models;
using Assignment4B.DAL.Repositories;
using Assignment4B.Utilities;
using FileBase = Assignment4B.DAL.Models.FileBase;

namespace Assignment4B.BLL
{
    public abstract class BaseManager<T>
    {        
        public abstract Dictionary<string, string> Save();
        // TODO Since this is the same for both album and slideshow,
        // can I make it general? Files is a list of generic type though
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
        // TODO Add public bool MoveItem(int oldPos, int newPos) to this instead of two implementations
        
    }
}
