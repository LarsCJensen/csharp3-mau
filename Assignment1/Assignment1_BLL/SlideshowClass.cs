using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL
{
    public class SlideshowClass: BaseClass, IFileCollectionManager
    {        
        public int LengthInSeconds { get; }
        public int Interval { get; private set; }        
        
        public void Play()
        {
            // TODO Play slideshow
        }

        public bool AddFile(FileClass file)
        {
            Files.Add(file);
            return true;
        }

        public bool DeleteFile(FileClass file)
        {
            Files.Remove(file);
            return true;
        }
        public int Count()
        {
            throw new NotImplementedException();
        }

    }
}
