using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoveYourBudget
{
    [Table("Budget")]
    public class Budget : BaseModel
    {
        [Required]
        public string Year { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public int Income { get; set; }        
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<BudgetRow> BudgetRows { get; set; }
    }
}