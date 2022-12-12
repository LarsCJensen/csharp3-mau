using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    public class IBlock: Block
    {
        /// <summary>
        /// Definition of block positions
        /// </summary>
        private readonly Position[][] form = new Position[][]
        {
            // state == 0
            new Position[] { new Position(1, 0), new Position(1,1), new Position(1, 2), new Position(1,3)},
            // state == 1
            new Position[] { new Position(0, 2), new Position(1,2), new Position(2, 2), new Position(3,2)},
            // state == 2
            new Position[] { new Position(2, 0), new Position(2,1), new Position(2, 2), new Position(2,3)},
            // state == 3
            new Position[] { new Position(0, 1), new Position(1,1), new Position(2, 1), new Position(3,1)}
        };
        public override int Id => 1;
        // Have the start position be in the row above the actual game field
        protected override Position StartPosition => new Position(0, 3);
        protected override Position[][] Form => form;
    }
}
