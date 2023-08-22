using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using QuickGrid.API.Data.Entities;
using System.Reflection.Metadata;

namespace QuickGrid.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=QuickGrid.db");
            options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(GenerateSeedData(10000));
        }

        private List<Customer> GenerateSeedData(int count)
        {
            var postFaker = new Faker<Customer>()
                .RuleFor(c => c.Id, _ => Guid.NewGuid())
                .RuleFor(c => c.FirstName, c => c.Name.FirstName())
                .RuleFor(c => c.LastName, c => c.Name.LastName())
                .RuleFor(c => c.Email, c => c.Internet.Email())
                .RuleFor(c => c.UserName, c => c.Internet.UserName())
                .RuleFor(c => c.Avatar, c => c.Internet.Avatar());

            var customers = postFaker.Generate(count);

            return customers;
        }
    }
}
