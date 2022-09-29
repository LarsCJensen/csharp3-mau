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
using File = Assignment2.DAL.Models.File;

// TODO IS THIS A MANAGER??

namespace Assignment2.BLL
{
    public abstract class BaseManager: ICollectionManager<File>
    {
        // TODO is the ICollectionmanager even needed?
        //public string? Title { get; set; }
        //public string? Description { get; set; }
        public List<File> Files { get; set; } = new List<File>();
        
        public bool AddItem(File file)
        {
            Files.Add(file);
            return true;
        }
        public bool DeleteItem(int position)
        {            
            Files.RemoveAt(position);
            return true;
        }
        public File GetItemAt(int pos)
        {
            return Files[pos];
        }
        // Not implemented
        public int CountItems()
        {            
            throw new NotImplementedException();
        }

        public bool MoveItem(int oldPos, int newPos)
        {
            Files = Utilities.Utilities.Move(Files, oldPos, newPos);
            return true;
        }
        public abstract bool Save();
    }
}
