using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoveYourBudget
{
    /// <summary>
    /// Table Loans
    /// </summary>
    [Table("Loans")]
    public class Loan : BaseModel
    {
        [Required]
        public string Institute { get; set; }
        [Required]
        public int Amount { get; set; }
        public int Mortgage { get; set; }
        [Required]
        public double InterestRate { get; set; }
        [Required]
        public DateTime LockInPeriod { get; set; }
    }
}