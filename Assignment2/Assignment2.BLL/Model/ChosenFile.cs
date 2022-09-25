using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL
{
    public class ChosenFile: File
    {   
        /// <summary>
        /// Class for chosen file, which has more attributes
        /// </summary>
        public int Position { get; set; }
        public string? Description { get; set; }
        public ChosenFile(File file, int position) : base(file.FileInfo)
        {
            this.Position = position;
        }
    }
}
