using GalaSoft.MvvmLight.Command;
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
    /// <summary>
    /// ViewModel for Loans
    /// </summary>
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
        private double _monthlyMortgage;
        public double MonthlyMortgage
        {
            get
            {
                return _monthlyMortgage;
            }
            set
            {
                _monthlyMortgage = value;
                OnPropertyChanged("MonthlyMortgage");
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
        private Loan _selectedLoan;
        public Loan SelectedLoan
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
        #region Commands
        public RelayCommand DeleteCommand { get; private set; }
        #endregion
        public LoansViewModel()
        {
            LoanManager = new LoanManager();
            RefreshGUI();
        }

        private void RefreshGUI()
        {
            LoadLoans();
            CalculateTotalAmount();
            CalculateAvgInterest();
            CalculateMortgage();
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            DeleteCommand = new RelayCommand(Delete);            
        }

        private void CalculateMortgage()
        {
            if (Loans.Count > 0)
            {
                MonthlyMortgage = Loans.Sum(x => x.Mortgage);
            }
        }

        private void CalculateAvgInterest()
        {
            if(Loans.Count > 0)
            {
                AvgInterest = Loans.Average(x => x.InterestRate);
            }            
        }

        private void CalculateTotalAmount()
        {
            if (Loans.Count > 0)
            {
                TotalLoanSum = Loans.Sum(x => x.Amount);
            }
        }

        private void LoadLoans()
        {
            try
            {
                // Since this is a I/O bound task Task.Run is used
                Task getLoansTask = Task.Run(() =>
                {
                    LoanManager.LoadLoans();
                });
                getLoansTask.Wait();
                Loans = new ObservableCollection<Loan>(LoanManager.Loans);                
            }
            catch (Exception ex)
            {
                // FUTURE should use dialog
                MessageBox.Show($"Could not get expenses: {ex.Message} ");
            }
        }
        public void OnSave(object sender, EventArgs e)
        {
            MessageBox.Show("Loan saved!", "Save completed!", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshGUI();
        }
        private void Delete()
        {
            if (SelectedLoan == null)
            {
                MessageBox.Show("Please select loan to delete!", "No loan selected!");
                return;
            }
            try
            {
                LoanManager.DeleteLoan(SelectedLoan.Id);
                RefreshGUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete expense: {ex.Message} ");
            }
        }        
    }
}
