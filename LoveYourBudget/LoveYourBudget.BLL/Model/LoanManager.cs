using LoveYourBudget.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.BLL.Model
{
    /// <summary>
    /// Manager for Loans
    /// </summary>
    public class LoanManager
    {
        private LoansService _loansService = new LoansService();
        public Loan Loan { get; set; }
        public List<Loan> Loans { get; set; }
        public LoanManager() 
        {
            Loan = new Loan();
            Loans = new List<Loan>();
        }
        /// <summary>
        /// Method to save loan
        /// </summary>
        public void SaveLoan()
        {
            if (Loan.Id == 0)
            {
                Loan.CreatedTime = DateTime.Now;
            }
            _loansService.Save(Loan);
        }
        /// <summary>
        /// Method to delete loan
        /// </summary>
        /// <param name="loanId">Id of loan</param>
        public void DeleteLoan(int loanId)
        {
            _loansService.Delete(loanId);
        }
        /// <summary>
        /// Method to Load Loans into List
        /// </summary>
        public void LoadLoans()
        {
            Loans = _loansService.GetItems().ToList();
        }
        /// <summary>
        /// Method to load a loan into Loan
        /// </summary>
        /// <param name="loanId">Id of loan</param>
        public void LoadLoan(int loanId)
        {
            Loan = _loansService.GetById(loanId);
        }
    }    
}
