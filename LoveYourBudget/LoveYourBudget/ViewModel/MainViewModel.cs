using GalaSoft.MvvmLight.Command;
using LoveYourBudget.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoveYourBudget.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BudgetManager _budgetManager;
        //public BudgetManager BudgetManager
        //{
        //    get { return _budgetManager; }
        //    set
        //    {
        //        _budgetManager = value;
        //        OnPropertyChanged("BudgetManager");
        //    }
        //}
        private string _title = "Love Your Budget";
        public string Title
        {
            get
            {
                return _title;
            }
        }
        private string _yearMonth;
        public string YearMonth
        {
            get
            {
                return _yearMonth;
            }
            set
            {
                _yearMonth = value;
                OnPropertyChanged("YearMonth");
            }
        }
        public int Income
        {
            get { return _budgetManager.Budget.Income; }
        }
        public int BudgetExpenses
        {
            get { return _budgetManager.GetSumBudgetExpenses(); }
        }
        public int ActualExpenses
        {
            get { return _budgetManager.GetSumExpenses(); }
        }
        public string TopExpenseCategory
        {
            get { return _budgetManager.GetTopExpenseCategory(); }
        }
        public MainViewModel()
        {
            _budgetManager = new BudgetManager();
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
        }
        /// <summary>
        /// Handler for OnSave event. Used to reload budget view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSave(object sender, EventArgs e)
        {
            MessageBox.Show("Saved!");
        }
    }
}
