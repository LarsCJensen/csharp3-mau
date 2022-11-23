using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Model
{
    public class Position
    {
        /// <summary>
        /// Class for holding a blocks position
        /// </summary>
        public int Row { get; set; }
        public int Column { get; set; }
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}

