using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL
{
    public class AlbumClass: BaseClass, IFileCollectionManager
    {
        
        public string Description { get; set; }
        
        public AlbumClass()
        {
            Files = new List<FileClass>();
        }   
       
        public bool Add(FileClass file)
        {
            // TODO try/except?
            Files.Add(file);
            return true;
        }

        public bool Delete(FileClass file)
        {
            // TODO try/except?
            Files.Remove(file);
            return true;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
