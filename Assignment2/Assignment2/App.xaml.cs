using Assignment2.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // TODO Upgrade if migration
            //string config = ConfigurationManager.ConnectionStrings["MyLocalDB"].ToString();
            //var builder = new DbContextOptionsBuilder<MediaPlayerDbContext>();
            //builder.UseSqlServer(config);
            //// TODO Use this to upgrade
            MediaPlayerDbContext context = new MediaPlayerDbContext();
            context.Database.EnsureCreated();
            base.OnStartup(e);
        }

    }
}
