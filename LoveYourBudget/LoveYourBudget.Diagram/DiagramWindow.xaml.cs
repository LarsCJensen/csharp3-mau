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
using LoveYourBudget.BLL.Model;
using static System.Net.Mime.MediaTypeNames;
using static LoveYourBudget.Diagram.Enums;

namespace LoveYourBudget.Diagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DiagramWindow : Window
    {
        VisualHost _visualHost;
        string _reportName;
        BudgetManager _budgetManager;
        
        public DiagramWindow(string reportName)
        {
            InitializeComponent();
            _reportName = reportName;
            cboYears.ItemsSource = new List<string>()
            {
                "2023",
                "2022",
                "Rolling 12m"
            };
            cboYears.SelectedItem = "2023";
            _budgetManager = new BudgetManager(cboYears.SelectedItem.ToString());
        }
        private async void DrawReport()
        {
            if (_reportName == "ExpensesVsBudget")
            {
                _visualHost = new VisualHost(diagramCanvas.ActualHeight);
                await DrawExpensesVsBudgetAsync();
            }
        }
        /// <summary>
        /// Method to draw report async
        /// </summary>
        /// <param name="year">year to draw for</param>
        /// <returns></returns>
        private async Task DrawExpensesVsBudgetAsync()
        {
            double xMax = 12;            
            List<string> xLabels = new List<string>()
            {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
            };

            double yMax = 20000;            
            List<string> yLabels = new List<string>()
            {
                "0", "2000", "4000", "6000", "8000", "10000", "12000", "14000", "16000", "18000", "20000"                
            };

            await DrawScaleAxis(xLabels, xMax, diagramCanvas.ActualWidth, Enums.Orientation.Horizontal);
            await DrawScaleAxis(yLabels, yMax, diagramCanvas.ActualHeight, Enums.Orientation.Vertical);
            
            PointCollection budgetPoints = GetBudgetSumPerMonth();
            await DrawPoints(budgetPoints, Brushes.Black);
            PointCollection expensePoints = GetExpensesSumPerMonth();
            await DrawPoints(expensePoints, Brushes.Red);
            await DrawLegend(); 
            diagramCanvas.Children.Add(_visualHost);
        }
        private async Task DrawScaleAxis(List<string> labels, double max, double size, Enums.Orientation orientation)
        {
             await _visualHost.DrawScaleAxis(labels, max, size, orientation);            
        }
        private async Task DrawPoints(PointCollection points, Brush color)
        {
            await _visualHost.DrawPointsAsync(points, color);
        }
        private async Task DrawLegend()
        {
            Dictionary<string, Brush> labels = new Dictionary<string, Brush>()
            {
                {"Budget", Brushes.Black },
                { "Expenses", Brushes.Red }
            };
            await _visualHost.DrawLegend(labels);
        }
        #region Helper methods
        /// <summary>
        /// Helper to get budget sum per month of chosen year
        /// </summary>
        /// <returns></returns>
        private PointCollection GetBudgetSumPerMonth()
        {
            PointCollection budgetPoints = new PointCollection();
            foreach (Budget budget in _budgetManager.Budgets)
            {
                // Month will be the X value in the diagram, but starting point in diagram is 0 so -1
                int xPoint = Int32.Parse(budget.Month) - 1;
                // Y value will be sum of budget
                budgetPoints.Add(new Point(xPoint, budget.BudgetRows.Sum(x => x.Amount)));
            }
            return budgetPoints;            
        }
        /// <summary>
        /// Helper method to get expenses summed per month of selected year
        /// </summary>
        /// <returns></returns>        
        private PointCollection GetExpensesSumPerMonth()
        {
            // TODO Make into a task?
            PointCollection expensesPoints = new PointCollection();
            // Get expenses per month
            for (int i = 1; i < 13; i++)
            {
                List<ExpenseRow> expenses = _budgetManager.GetExpenses(cboYears.SelectedItem.ToString(), (i).ToString()).ToList();
                // Month will be the X value in the diagram, but starting point in diagram is 0 so -1
                if (expenses.Count == 0)
                {
                    continue;
                }
                int xPoint = i - 1;
                // Y value will be sum of expense
                expensesPoints.Add(new Point(xPoint, expenses.Sum(x => x.Amount)));
            }

            //foreach (ExpenseRow expense in expenses)
            //{
            //    // Month will be the X value in the diagram, but starting point in diagram is 0 so -1
            //    int xPoint = Int32.Parse(expense.Date.ToString()) - 1;
            //    // Y value will be sum of budget
            //    //expensesPoints.Add(new Point(xPoint, expense.Sum(x => x.Amount)));
            //}
            return expensesPoints;            
        }
        // Helper to clear children from canvas
        private void ClearCanvas()
        {
            diagramCanvas.Children.Clear();
            _visualHost = new VisualHost(diagramCanvas.ActualHeight);
        }
        #endregion 
        #region Events
        // Event for Window size changed
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(this.IsLoaded)
            {
                // Reset visualHost
                diagramCanvas.Children.Clear();
                _visualHost = new VisualHost(diagramCanvas.ActualHeight);
                DrawReport();
            }            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawReport();
        }

        private async void cboYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (!comboBox.IsLoaded)
                return;
            if (comboBox != null)
            {
                string year = comboBox.SelectedItem.ToString();
                ClearCanvas();
                _budgetManager = new BudgetManager(year);
                await DrawExpensesVsBudgetAsync();
            }            
        }
        #endregion        
    }
}
