using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoveYourBudget
{
    [Table("ExpenseRows")]
    public class ExpenseRow : BaseModel
    {
        [Required]
        public virtual Category Category { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}