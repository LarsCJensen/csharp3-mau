using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Models
{
    public class Album: Base
    {
        // No properties yet
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<File> Files { get; set; }

    }
}
