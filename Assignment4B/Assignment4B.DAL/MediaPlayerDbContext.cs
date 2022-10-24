using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Assignment4B.DAL.Models;
using FileBase = Assignment4B.DAL.Models.FileBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Assignment4B.DAL
{
    public class MediaPlayerDbContext: DbContext
    {
        /// <summary>
        /// Database context for app
        /// </summary>
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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=Assignment4B.DAL.MediaPlayerContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            // To use LazyLoading of relationships
            options.UseLazyLoadingProxies();            
        }
    }

    public class MediaPlayerDbContextFactory : IDesignTimeDbContextFactory<MediaPlayerDbContext>
    {
        public MediaPlayerDbContext CreateDbContext(string[] args)
        {            
            var builder = new DbContextOptionsBuilder<MediaPlayerDbContext>();
            var connectionString = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=Assignment4B.DAL.MediaPlayerContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            builder.UseSqlServer(connectionString);
            return new MediaPlayerDbContext(builder.Options);
        }
    }

}
   

