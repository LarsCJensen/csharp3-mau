using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Models
{
    /// <summary>
    /// Slideshow database object
    /// </summary>
    public class Slideshow : Base
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<File> files { get; set; }
        public int Interval { get; set; }
        public int LengthInSeconds { get; set; }
        
        //// Foreign Key
        //public int AuthorId { get; set; }
        //// Navigation property
        //public Author Author { get; set; }
    }
}
