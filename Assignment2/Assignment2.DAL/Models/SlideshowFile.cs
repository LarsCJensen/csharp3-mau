using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Models
{
    [Table("SlideshowFiles")]
    public class SlideshowFile: FileBase
    {
        public int SlideshowId { get; set; }
        public virtual Slideshow Slideshow { get; set; }

    }
}
