using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LoveYourBudget
{
    /// <summary>
    /// Base class
    /// </summary>

    public class BaseModel
    {
        // Let id be incremented automatically and used as key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.Now;                       
    }
}