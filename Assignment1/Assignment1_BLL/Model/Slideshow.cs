using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL
{
    public class Slideshow: Base 
    {        
        public int LengthInSeconds { get; set; }
        public int Interval { get; set; } = 3;                
    }
}
