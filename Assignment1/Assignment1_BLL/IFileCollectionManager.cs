using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL
{
    /// <summary>
    /// Interface for FileCollectionManager which includes required properties and methods
    /// </summary>
    public interface IFileCollectionManager
    {
        public List<FileClass> Files { get; set; }
        public bool Add(FileClass file);
        public bool Delete(FileClass file);
        public int Count();
    }
}
