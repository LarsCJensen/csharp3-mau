using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL
{
    public class Album: Base, ICollectionManager<ChosenFile>
    {
        
        public string? Description { get; set; }        
        
        public bool AddItem(ChosenFile file)
        {
            // TODO try/except?
            Files.Add(file);
            return true;
        }

        public bool DeleteItem(int position)
        {
            // TODO try/except?
            Files.RemoveAt(position);
            return true;
        }

        public ChosenFile GetItemAt(int pos)
        {
            return Files[pos];
        }

        public int CountItems()
        {
            throw new NotImplementedException();
        }

        
    }
}
