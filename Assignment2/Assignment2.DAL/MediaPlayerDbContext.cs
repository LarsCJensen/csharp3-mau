using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Assignment2.DAL.Models;
using File = Assignment2.DAL.Models.File;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Assignment2.DAL
{
    public class MediaPlayerDbContext: DbContext
    {
        public MediaPlayerDbContext()
        {

        }
        public MediaPlayerDbContext(DbContextOptions<MediaPlayerDbContext> options) : base(options) 
        {
            // TODO Remove
            Console.WriteLine("test");
        }
        public DbSet<File> Files { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Slideshow> Slideshows{ get; set; }

        // TODO Not used
        //private static DbContextOptions GetOptions(DbContextOptionsBuilder<MediaPlayerDbContext> options)
        //{
        //    return SqlServerDbContextOptionsExtensions.UseSqlServer(options).Options;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=Assignment2.DAL.MediaPlayerContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
        }
    }

    public class MediaPlayerDbContextFactory : IDesignTimeDbContextFactory<MediaPlayerDbContext>
    {
        public MediaPlayerDbContext CreateDbContext(string[] args)
        {            
            var builder = new DbContextOptionsBuilder<MediaPlayerDbContext>();
            var connectionString = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=Assignment2.DAL.MediaPlayerContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            builder.UseSqlServer(connectionString);
            return new MediaPlayerDbContext(builder.Options);
        }
    }

}
   

