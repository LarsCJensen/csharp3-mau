using LoveYourBudget.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoveYourBudget.ViewModel
{
    public class LoansViewModel: BaseViewModel
    {
        private LoanManager _loanManager;
        public LoanManager LoanManager
        {
            get { return _loanManager; } 
            set
            {
                _loanManager = value;
                OnPropertyChanged("LoanManager");
            }
        }
        private double _totalLoanSum;
        public double TotalLoanSum
        {
            get
            {
                return _totalLoanSum;
            }
            set
            {
                _totalLoanSum = value;
                OnPropertyChanged("TotalLoanSum");
            }
        }
        private double _avgInterest;
        public double AvgInterest
        {
            get
            {
                return _avgInterest;
            }
            set
            {
                _avgInterest = value;
                OnPropertyChanged("AvgInterest");
            }
        }
        private double _yearlyMortgage;
        public double YearlyMortgage
        {
            get
            {
                return _yearlyMortgage;
            }
            set
            {
                _yearlyMortgage = value;
                OnPropertyChanged("YearlyMortgage");
            }
        }
        private ObservableCollection<Loan> _loans = new ObservableCollection<Loan>();
        public ObservableCollection<Loan> Loans
        {
            get
            {
                return _loans;
            }
            set
            {
                _loans = value;
                OnPropertyChanged("Loans");
            }
        }
        private string _selectedLoan;
        public string SelectedLoan
        {
            get
            {
                return _selectedLoan;
            }
            set
            {
                _selectedLoan = value;
                OnPropertyChanged("SelectedLoan");
            }
        }

        public LoansViewModel()
        {
            LoanManager = new LoanManager();
            RefreshGUI();
        }
        private void RefreshGUI()
        {
            LoadLoans();
        }
        private void LoadLoans()
        {
            try
            {
                // TODO Är detta rätt iom att det är en I/O-operation?
                // Borde kanske vara  Task loadExpenses = Task.Run()
                // Since this is a I/O bound task Task.Run is used
                Task getLoansTask = Task.Run(() =>
                {
                    Loans = new ObservableCollection<Loan>(LoanManager.Loans);
                });
                //ExpenseRows = await Task.Run(() => new ObservableCollection<ExpenseRow>(BudgetManager.GetExpensesAsync(SelectedYear, SelectedMonth).Result));
                getLoansTask.Wait();
                //ActualExpenses = ExpenseRows.Sum(x => x.Amount);
            }
            catch (Exception ex)
            {
                // TODO should use dialog
                MessageBox.Show($"Could not get expenses: {ex.Message} ");
            }
        }
    }
}
