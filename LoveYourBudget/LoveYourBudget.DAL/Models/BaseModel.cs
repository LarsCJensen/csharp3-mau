﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LoveYourBudget
{
    public class BaseModel
    {
        /// <summary>
        /// Base class
        /// </summary>
        // Let id be incremented automatically and used as key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.Now;                       
    }
}