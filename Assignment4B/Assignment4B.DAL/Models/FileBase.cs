using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4B.DAL.Models
{
    /// <summary>
    /// File Database Object
    /// </summary>
    public class FileBase : Base
    {
        [Required]
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullName { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Position { get; set; }        
    }
}
