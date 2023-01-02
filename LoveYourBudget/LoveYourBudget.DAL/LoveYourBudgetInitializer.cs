using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// TODO REMOVE
namespace LoveYourBudget.DAL
{
    /// <summary>
    /// Database initializer to create records on database create
    /// </summary>
    public class LoveYourBudgetInitializer 
    {
        private readonly ModelBuilder modelBuilder;

        public LoveYourBudgetInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Category>().HasData(
                   new Category() { Id = 1, Name = "Groceries", CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now },
                   new Category() { Id = 2, Name = "Phone", CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now },
                   new Category() { Id = 3, Name = "Electricity", CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now },
                   new Category() { Id = 4, Name = "Gas", CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now },
                   new Category() { Id = 5, Name = "Broadband", CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now },
                   new Category() { Id = 6, Name = "Streaming", CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now },
                   new Category() { Id = 7, Name = "Transportation", CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now },
                   new Category() { Id = 8, Name = "Restaurants", CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now }
            );
        }        
    }   
}
