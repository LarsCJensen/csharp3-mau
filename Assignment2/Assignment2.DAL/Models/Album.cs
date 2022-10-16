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
        /// <summary>
        /// Album model
        /// </summary>
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfImages { get; set; }
        public int NumberOfVideos { get; set; }
        public virtual ICollection<AlbumFile> Files { get; set; }
        
    }
}
