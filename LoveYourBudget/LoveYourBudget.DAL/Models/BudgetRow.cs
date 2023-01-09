using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace LoveYourBudget
{
    [Table("BudgetRow")]
    public class BudgetRow : BaseModel
    {
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }
    }
}