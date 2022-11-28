using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Model
{
    public class LBlock: Block
    {
        private readonly Position[][] form = new Position[][]
        {
            // state == 0
            new Position[] { new Position(0, 2), new Position(1,0), new Position(1, 1), new Position(1,2)},
            // state == 1
            new Position[] { new Position(0, 1), new Position(1,1), new Position(2, 1), new Position(2,2)},
            // state == 2
            new Position[] { new Position(1, 0), new Position(1,1), new Position(1, 2), new Position(2,0)},
            // state == 3
            new Position[] { new Position(0, 0), new Position(0,1), new Position(1, 1), new Position(2,1)}
        };
        public override int Id => 3;
        protected override Position StartPosition => new Position(0,3);
        protected override Position[][] Form => form;        
    }
}