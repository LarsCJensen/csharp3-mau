﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoveYourBudget
{
    /// <summary>
    /// Table ExpenseRows
    /// </summary>
    [Table("ExpenseRows")]
    public class ExpenseRow : BaseModel
    {
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}