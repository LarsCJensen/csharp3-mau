using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Model
{
    public class OBlock: Block
    {
        private readonly Position[][] form = new Position[][]
        {
            new Position[] { new Position(0,0), new Position(0,1), new Position(1,0), new Position(0,4) }
        };
        public override int Id => 4;
        protected override Position StartPosition => new Position(0,4);
        protected override Position[][] Form => form;

        protected override Color Color => Color.Yellow;
    }
}

// TODO ADd block queue for next block 