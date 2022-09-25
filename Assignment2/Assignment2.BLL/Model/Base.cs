using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.BLL.Interface;
using Assignment2.Utilities;

namespace Assignment2.BLL
{
    public class Base: ICollectionManager<ChosenFile>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<ChosenFile> Files { get; set; } = new List<ChosenFile>();
        public bool AddItem(ChosenFile file)
        {
            Files.Add(file);
            return true;
        }
        public bool DeleteItem(int position)
        {            
            Files.RemoveAt(position);
            return true;
        }
        public ChosenFile GetItemAt(int pos)
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
    }
}
