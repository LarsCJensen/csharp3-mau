using LoveYourBudget.Diagram;
using LoveYourBudget.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoveYourBudget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel vm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void NewBudget_Click(object sender, RoutedEventArgs e)
        {
            BudgetViewModel newVm = new BudgetViewModel();
            BudgetWindow newBudgetWindow = new BudgetWindow();
            newBudgetWindow.DataContext = newVm;
            newVm.OnClose += delegate { newBudgetWindow.Close(); };
            newVm.OnSave += vm.OnSave;
            newBudgetWindow.Show();
        }
        private void EditOrCreateBudget_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Content.ToString() == "Create budget")
            {
                BudgetWindow newBudgetWindow = new BudgetWindow();
                BudgetViewModel newVm = new BudgetViewModel(vm.SelectedYear, vm.SelectedMonth);                
                newBudgetWindow.DataContext = newVm;
                newVm.OnClose += delegate { newBudgetWindow.Close(); };
                newVm.OnSave += vm.OnSave;
                newBudgetWindow.Show();
            }
            else if (btn.Content.ToString() == "Edit budget")
            {
                BudgetWindow budgetWindow = new BudgetWindow();
                BudgetViewModel budgetVm = new BudgetViewModel(vm.BudgetManager);
                budgetWindow.DataContext = budgetVm;
                budgetVm.OnClose += delegate { budgetWindow.Close(); };
                budgetVm.OnSave += vm.OnSave;
                budgetWindow.Show();
            }

        }

        private void ExpensesVsBudget_Click(object sender, RoutedEventArgs e)
        {            
            DiagramWindow diagramWindow = new DiagramWindow("ExpensesVsBudget");
            diagramWindow.Title = "Expenses vs Budget";
            diagramWindow.Show();
        }
        private void ExpensesPerCategory_Click(object sender, RoutedEventArgs e)
        {
            DiagramWindow diagramWindow = new DiagramWindow("ExpensesPerCategory");
            diagramWindow.Title = "Expenses per category";
            diagramWindow.Show();
        }
        
        private void ViewLoans_Click(object sender, RoutedEventArgs e)
        {
            LoansWindow loansWindow = new LoansWindow();
            LoansViewModel loansVm = new LoansViewModel();
            loansWindow.DataContext = loansVm;
            loansVm.OnClose += delegate { loansVm.Close(); };
            loansWindow.Show();
        }
    }
}
