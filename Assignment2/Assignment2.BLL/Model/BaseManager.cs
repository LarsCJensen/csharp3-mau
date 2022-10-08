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

// TODO IS THIS A MANAGER??

namespace Assignment2.BLL
{
    public abstract class BaseManager<T>
    {
        // TODO is the ICollectionmanager even needed?
        //public string? Title { get; set; }
        //public string? Description { get; set; }
        //public List<T> Files { get; set; } = new List<T>();
        
        //public bool AddItem(T file)
        //{
        //    Files.Add(file);
        //    return true;
        //}
        //public bool DeleteItem(int position)
        //{            
        //    Files.RemoveAt(position);
        //    return true;
        //}
        //public FileBase GetItemAt(int pos)
        //{
        //    return Files[pos];
        //}
        //// Not implemented
        //public int CountItems()
        //{            
        //    throw new NotImplementedException();
        //}

        //public bool MoveItem(int oldPos, int newPos)
        //{
        //    Files = Utilities.Utilities.Move(Files, oldPos, newPos);
        //    return true;
        //}
        public abstract Dictionary<string, string> Save();
        public abstract bool Delete(int id);
        public abstract List<T> GetItems();
        public int GetCount(List<string> filesExtensions, List<string> extensions)
        {
            var extensionsCount = filesExtensions.Count(f => extensions.Contains(f));
            return extensionsCount;
        }
        public abstract List<T> SearchItems(string searchText, string searchProperty);
    }
}
