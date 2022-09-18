using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_BLL.Model
{
    /// <summary>
    /// File Database Object
    /// </summary>
    public class FileObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        [Required]
        public int Position { get; set; }
    }
}
