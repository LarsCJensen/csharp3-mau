using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DAL
{
    // TODO REMOVE
    public class DBInitializer
    {
        private readonly MediaPlayerDbContext _context;

        public DBInitializer(MediaPlayerDbContext context)
        {
            _context = context;
        }

        public void Run()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // TODO Later
            //_context.Database.Migrate();
        }
    }
}
