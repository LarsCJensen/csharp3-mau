using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL.Model
{
    /// <summary>
    /// Slideshow DTO used for get
    /// </summary>
    public class SlideshowDto
    {
        public string Title { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public virtual ICollection<string> files { get; set; }
    }
}
