using GalaSoft.MvvmLight.Command;
using LoveYourBudget.BLL.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoveYourBudget.ViewModel
{
    /// <summary>
    /// ViewModel for Loan
    /// </summary>
    public class LoanViewModel: BaseViewModel
    {
        #region Properties
        private LoanManager _loanManager;
        public LoanManager LoanManager
        {
            get
            {
                return _loanManager;
            }
            set
            {
                _loanManager = value;
                OnPropertyChanged("LoanManager");
            }
        }
        #endregion
        #region Commands
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }
        #endregion
        #region EventHandlers
        public event EventHandler OnSave;
        #endregion
        public LoanViewModel() 
        { 
            LoanManager = new LoanManager();
            LoanManager.Loan.LockInPeriod = DateTime.Now;
        }
        public LoanViewModel(int loanId)
        {
            LoanManager = new LoanManager();
            LoanManager.LoadLoan(loanId);
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            SaveCommand = new RelayCommand(Save);
            ExitCommand = new RelayCommand(Close);
        }
        private void Save()
        {
            if(ValidateLoan())
            {
                try
                {
                    LoanManager.SaveLoan();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    MessageBox.Show(ex.Message, "Save error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                OnSave(this, EventArgs.Empty);
                Close();
            }          
        }

        private bool ValidateLoan()
        {
            // I only check if institute is set. If user inputs errorous amount etc. it will be translated to 0
            if(LoanManager.Loan.Institute == "" || LoanManager.Loan.Institute == null)
            {
                MessageBox.Show("You must enter an institute", "No institute!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }            
            return true;
        }
    }
}
