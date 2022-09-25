using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL.Interface
{
    /// <summary>
    /// Interface for FileCollectionManager which includes required properties and methods
    /// </summary>
    public interface ICollectionManager<T>
    {
        /// <summary>
        /// Method to Add item to collection
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Success or failed</returns>
        public bool AddItem(T item);
        /// <summary>
        /// Method to Delete Item from collection
        /// </summary>
        /// <param name="pos">Position to delete</param>
        /// <returns>Success or failed</returns>
        public bool DeleteItem(int pos);
        /// <summary>
        /// Method to get Item at position
        /// </summary>
        /// <param name="pos">Position to delete</param>
        /// <returns>Item at position</returns>
        public T GetItemAt(int pos);
        /// <summary>
        /// Method to get number of files in collection
        /// </summary>
        /// <returns>Number of files in collection</returns>
        public int CountItems();
        public bool MoveItem(int oldPos, int newPos);
    }
}
