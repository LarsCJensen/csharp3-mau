using LoveYourBudget.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LoveYourBudget
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LoveYourBudgetDbContext context = new LoveYourBudgetDbContext();
            context.Database.EnsureCreated();
            base.OnStartup(e);
        }
    }
}
