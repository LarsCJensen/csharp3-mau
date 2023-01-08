using LoveYourBudget.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveYourBudget.BLL.Model
{
    
    public class LoanManager: BaseManager
    {
        private LoansService _loansService = new LoansService();
        public Loan Loan { get; set; }
        public List<Loan> Loans { get; set; }
        public LoanManager() 
        {
            Loan = new Loan();
            Loans = new List<Loan>();
        }
        public void SaveLoan()
        {
            if (Loan.Id == 0)
            {
                Loan.CreatedTime = DateTime.Now;
            }
            _loansService.Save(Loan);
        }
        public void DeleteLoan(int loanId)
        {
            _loansService.Delete(loanId);
        }
        public void LoadLoans()
        {
            Loans = _loansService.GetItems().ToList();
        }
        public void LoadLoan(int loanId)
        {
            Loan = _loansService.GetById(loanId);
        }
    }    
}
