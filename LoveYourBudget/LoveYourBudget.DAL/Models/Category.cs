﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoveYourBudget
{
    /// <summary>
    /// Table Category
    /// </summary>
    public class Category : BaseModel
    {
        public string Name { get; set; }       
        public virtual ICollection<BudgetRow> BudgetRows { get; set; }
        public virtual ICollection<ExpenseRow> ExpenseRows { get; set; }
    }
}