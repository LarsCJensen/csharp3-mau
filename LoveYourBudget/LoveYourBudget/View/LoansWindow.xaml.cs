using LoveYourBudget.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoveYourBudget.View
{
    /// <summary>
    /// Interaction logic for LoansWindow.xaml
    /// </summary>
    public partial class LoansWindow : Window
    {
        LoansViewModel vm = new LoansViewModel();
        public LoansWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
            vm.OnClose += delegate { vm.Close(); };

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            LoanViewModel newVm = new LoanViewModel();
            AddLoanWindow newLoanWindow = new AddLoanWindow();
            newLoanWindow.DataContext = newVm;
            newVm.OnClose += delegate { newLoanWindow.Close(); };
            newVm.OnSave += vm.OnSave;
            newLoanWindow.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {     
            if(vm.SelectedLoan == null)
            {
                MessageBox.Show("Please select loan to edit!", "No loan selected!");
                return;
            }
            LoanViewModel newVm = new LoanViewModel(vm.SelectedLoan.Id);
            AddLoanWindow newLoanWindow = new AddLoanWindow();
            newLoanWindow.DataContext = newVm;
            newVm.OnClose += delegate { newLoanWindow.Close(); };
            newVm.OnSave += vm.OnSave;
            newLoanWindow.Show();
        }
    }
}
