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
        private void DrawReportAsync()
        {
            if (_reportName == "ExpensesVsBudget")
            {
                _visualHost = new VisualHost(diagramCanvas.ActualHeight);
                DrawExpensesVsBudget();
            }else if(_reportName == "ExpensesPerCategory")
            {
                _visualHost = new VisualHost(diagramCanvas.ActualHeight);
                DrawExpensesPerCategory();
            }
        }
        /// <summary>
        /// Method to draw Expenses vs Budget report async
        /// </summary>        
        /// <returns>Task</returns>
        private void DrawExpensesVsBudget()
        {
            PointCollection budgetPoints = GetBudgetSumPerMonth();
            PointCollection expensePoints = GetExpensesSumPerMonth();
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
            
            if(expensePoints.Count == 0|| budgetPoints.Count == 0) {
                MessageBox.Show("No expense or budget rows to show for selected year!\nPlease choose another year to show.", "Nothing to show");
                DrawScaleAxis(xLabels, xMax, diagramCanvas.ActualWidth, Enums.Orientation.Horizontal);
                DrawScaleAxis(yLabels, yMax, diagramCanvas.ActualHeight, Enums.Orientation.Vertical);
                diagramCanvas.Children.Add(_visualHost);
                return; 
            }

            // Make scale dynamic
            if (expensePoints.Max(e => e.Y) > budgetPoints.Max(e => e.Y))
            {
                yMax = expensePoints.Max(e => e.Y);                
            } else
            {
                yMax = budgetPoints.Max(e => e.Y);
            }
            // Round up to closest 1000
            yMax = Math.Ceiling(yMax / 1000) * 1000;

            // Create labels dynamically
            yLabels = new List<string>() { "0" };
            double scaleStep = xMax < 10000 ? 500 : 1000;
            double yStep = yMax / 10;
            for (int i = 1; i < 11;i++)
            {
                yLabels.Add((i * Math.Ceiling((yMax / 10) / scaleStep) * scaleStep).ToString());
            }
            yMax = Int32.Parse(yLabels[yLabels.Count - 1]);
            DrawScaleAxis(xLabels, xMax, diagramCanvas.ActualWidth, Enums.Orientation.Horizontal);
            DrawScaleAxis(yLabels, yMax, diagramCanvas.ActualHeight, Enums.Orientation.Vertical);

            DrawPoints(expensePoints, Brushes.Red);
            DrawPoints(budgetPoints, Brushes.Black);


            Dictionary<string, Brush> labels = new Dictionary<string, Brush>()
            {
                {"Budget", Brushes.Black },
                { "Expenses", Brushes.Red }
            };

            DrawLegend(labels); 
            diagramCanvas.Children.Add(_visualHost);
        }
        /// <summary>
        /// Method to draw Expenses vs Budget report async
        /// </summary>        
        /// <returns>Task</returns>
        private void DrawExpensesPerCategory()
        {
            List<Category> categories = _budgetManager.GetCategories();
            // We will add an extra step to have the categories start one step to the right
            double xMax = categories.Count + 1;
            List<string> xLabels = categories.Select(c => c.Name).ToList();
            xLabels.Insert(0, "");
            double yMax = 20000;
            List<string> yLabels = new List<string>()
            {
                "0", "1000", "2000", "3000", "4000", "50000", "6000", "7000", "8000", "9000", "10000"
            };
            PointCollection expensePoints = GetExpensesSumPerCategory(categories);
            
            if (expensePoints.Count == 0)
            {
                MessageBox.Show("No expense rows to show for selected year!\nPlease choose another year to show.", "Nothing to show");
                DrawScaleAxis(xLabels, xMax, diagramCanvas.ActualWidth, Enums.Orientation.Horizontal);
                DrawScaleAxis(yLabels, yMax, diagramCanvas.ActualHeight, Enums.Orientation.Vertical);
                diagramCanvas.Children.Add(_visualHost);
                return;
            }
            yMax = expensePoints.Max(e => e.Y);
            // Create labels dynamically
            yLabels = new List<string>() { "0" };
            double scaleStep = xMax < 10000 ? 500 : 1000;
            double yStep = yMax / 10;
            for (int i = 1; i < 11; i++)
            {
                yLabels.Add((i * Math.Ceiling((yMax / 10) / scaleStep) * scaleStep).ToString());
            }
            yMax = Int32.Parse(yLabels[yLabels.Count - 1]);
            DrawScaleAxis(xLabels, xMax, diagramCanvas.ActualWidth, Enums.Orientation.Horizontal);
            DrawScaleAxis(yLabels, yMax, diagramCanvas.ActualHeight, Enums.Orientation.Vertical);
            
            DrawStack(expensePoints, Brushes.Red, 10);

            diagramCanvas.Children.Add(_visualHost);
        }
        /// <summary>
        /// Method to draw scale axis async
        /// </summary>
        /// <param name="labels">Labels to draw</param>
        /// <param name="max">Max value for axis</param>
        /// <param name="size">Size of canvas axis</param>
        /// <param name="orientation">Orientation</param>
        /// <returns></returns>
        private void DrawScaleAxis(List<string> labels, double max, double size, Enums.Orientation orientation)
        {
           _visualHost.DrawScaleAxis(labels, max, size, orientation);
            //await _visualHost.DrawScaleAxis(labels, max, size, orientation);            
        }
        /// <summary>
        /// Method to draw points async
        /// </summary>
        /// <param name="points">Points to draw</param>
        /// <param name="color">Color to use</param>
        /// <returns>Task</returns>
        private void DrawPoints(PointCollection points, Brush color, int size=1)
        {
            _visualHost.DrawPoints(points, color, size);
        }
        /// <summary>
        /// Method to draw points async
        /// </summary>
        /// <param name="points">Points to draw</param>
        /// <param name="color">Color to use</param>
        /// <returns>Task</returns>
        private void DrawStack(PointCollection points, Brush color, int size = 1)
        {
            _visualHost.DrawStack(points, color, size);
        }
        /// <summary>
        /// Method to draw legend async
        /// </summary>
        /// <param name="labels">Dict with labels and color</param>
        /// <returns>Task</returns>
        private async Task DrawLegend(Dictionary<string, Brush> labels)
        {
            await _visualHost.AsyncDrawLegend(labels);
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
                List<ExpenseRow> expenses = _budgetManager.GetExpensesForDate(cboYears.SelectedItem.ToString(), (i).ToString()).ToList();
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
        /// <summary>
        /// Helper method to get expenses summed per month of selected year
        /// </summary>
        /// <returns></returns>        
        private PointCollection GetExpensesSumPerCategory(List<Category> categories)
        {            
            // TODO Make into a task?
            PointCollection expensesPoints = new PointCollection();            
            // Get expenses per month
            for (int i = 0; i < categories.Count; i++)
            {
                List<ExpenseRow> expenses = _budgetManager.GetExpensesByYearAndCategory(cboYears.SelectedItem.ToString(), categories[i].Id).ToList();
                if (expenses.Count == 0)
                {
                    continue;
                }
                int xPoint = i+1;
                // Y value will be sum of expense
                expensesPoints.Add(new Point(xPoint, expenses.Sum(x => x.Amount)));
            }
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
                ClearCanvas();
                DrawReportAsync();
            }            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawReportAsync();
        }

        private void cboYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (!comboBox.IsLoaded)
                return;
            if (comboBox != null)
            {
                string year = comboBox.SelectedItem.ToString();
                if(year == "Rolling 12m")
                {
                    MessageBox.Show("Rolling 12 months has not been implemented yet!", "Sorry!");
                    return;
                }
                ClearCanvas();
                _budgetManager = new BudgetManager(year);
                //await DrawExpensesVsBudgetAsync();
                DrawReportAsync();
            }            
        }
        #endregion        
    }
}
