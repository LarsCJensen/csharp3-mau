using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL.Model
{
    /// <summary>
    /// Slideshow database object
    /// </summary>
    public class SlideshowObject
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public int LengthInSeconds { get; set; }
        public virtual ICollection<FileObject> files { get; set; }

        //// Foreign Key
        //public int AuthorId { get; set; }
        //// Navigation property
        //public Author Author { get; set; }
    }
}
