using Assignment3.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.BLL
{
    // TODO REMOVE
    public class SaveBug: EventArgs
    {
        public Bug Bug { get; set; }
        public SaveBug(Bug bug)
        {
            Bug = bug;
        }   
    }
}
