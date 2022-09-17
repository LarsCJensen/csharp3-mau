using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL
{
    public class Slideshow: Base, ICollectionManager<ChosenFile>
    {        
        public int LengthInSeconds { get; }
        public int Interval { get; set; }        
        
        public void Play()
        {
            // TODO Play slideshow
        }

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
        public int CountItems()
        {
            throw new NotImplementedException();
        }       
    }
}
