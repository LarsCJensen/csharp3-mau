using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL
{
    public class Base
    {
        public string? Name { get; set; }
        public List<ChosenFile> Files { get; set; } = new List<ChosenFile>();
    }
}
