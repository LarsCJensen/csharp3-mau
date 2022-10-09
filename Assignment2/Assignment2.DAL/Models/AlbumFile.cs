using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Models
{
    [Table("AlbumFiles")]
    public class AlbumFile: FileBase
    {
        /// Album files
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

    }
}
