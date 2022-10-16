using Assignment2.BLL.Interface;
using Assignment3.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.BLL
{
    // TODO Remove?
    public class BugManager: ICollectionManager<Bug>
    {
        public List<Bug> Bugs { get; set; } =  new List<Bug>();

        public BugManager()
        {

        }

        public bool AddItem(Bug item)
        {
            Bugs.Add(item);
            return true;
        }

        public bool DeleteItem(int pos)
        {
            Bugs.RemoveAt(pos);
            return true;
        }

        public Bug GetItemAt(int pos)
        {
            return Bugs[pos];
        }

        public bool MoveItem(int oldPos, int newPos)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            // TODO Save 
            return true;
        }

        public bool Load()
        {
            // TODO Save 
            return true;
        }
    }
}
