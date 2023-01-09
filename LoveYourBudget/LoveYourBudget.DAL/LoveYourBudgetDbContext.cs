using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LoveYourBudget.DAL
{
    /// <summary>
    /// DBContext for LoveYourBudget
    /// </summary>
    public class LoveYourBudgetDbContext: DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetRow> BudgetRows { get; set; }
        public DbSet<ExpenseRow> ExpenseRows { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=LoveYourBudget.DAL.LoveYourBudgetDBContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            // To use LazyLoading of relationships
            options.UseLazyLoadingProxies();            
        }
        public LoveYourBudgetDbContext() { }
        public LoveYourBudgetDbContext(DbContextOptions<LoveYourBudgetDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// Override to seed data and create composite key
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new LoveYourBudgetInitializer(modelBuilder).Seed();
            modelBuilder.Entity<Budget>().HasKey(nameof(Budget.Year), nameof(Budget.Month));
        }

    }

    public class LoveYourBudgetDbContextFactory : IDesignTimeDbContextFactory<LoveYourBudgetDbContext>
    {
        public LoveYourBudgetDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LoveYourBudgetDbContext>();
            var connectionString = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=LoveYourBudget.DAL.LoveYourBudgetDBContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            builder.UseSqlServer(connectionString);
            return new LoveYourBudgetDbContext(builder.Options);
        }
    }
}
