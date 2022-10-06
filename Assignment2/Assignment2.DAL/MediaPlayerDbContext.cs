using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Assignment2.DAL.Models;
using FileBase = Assignment2.DAL.Models.FileBase;
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
           
        }
        public DbSet<AlbumFile> AlbumFiles { get; set; }
        public DbSet<SlideshowFile> SlideshowFiles { get; set; }
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
            // To use LazyLoading of relationships
            options.UseLazyLoadingProxies();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) 
        //{
        //    modelBuilder.Entity<File>()
        //        .HasOne(a => a.Album)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
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
   

