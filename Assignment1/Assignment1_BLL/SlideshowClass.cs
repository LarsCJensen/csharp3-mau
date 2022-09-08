using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL
{
    public class SlideshowClass: IFileCollectionManager
    {
        public string Name { get; set; }
        public List<FileClass> Files { get; set; }

        public SlideshowClass(string name)
        {
            Name = name;
            Files = new List<FileClass>();
        }
        
        public void Play()
        {
            // TODO Play slideshow
        }

        public bool Add(FileClass file)
        {
            Files.Add(file);
            return true;
        }

        public bool Delete(FileClass file)
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
