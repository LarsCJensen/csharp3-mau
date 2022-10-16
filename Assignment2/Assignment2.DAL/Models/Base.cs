using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL.Models
{
    public class Base
    {
        /// <summary>
        /// Base class
        /// </summary>
        // Let id be incremented automatically and used as key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;        
    }
}
