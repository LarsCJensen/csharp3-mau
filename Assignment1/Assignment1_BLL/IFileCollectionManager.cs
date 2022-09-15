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
        /// <summary>
        /// Method to Add File to Files collection
        /// </summary>
        /// <param name="file">File to add</param>
        /// <returns>Success or failed</returns>
        public bool AddFile(FileClass file);
        /// <summary>
        /// Method to Delete File from Files collection
        /// </summary>
        /// <param name="file">File to delete</param>
        /// <returns>Success or failed</returns>
        public bool DeleteFile(FileClass file);
        /// <summary>
        /// Method to get number of files in collection
        /// </summary>
        /// <returns>Number of files in collection</returns>
        public int Count();
    }
}
