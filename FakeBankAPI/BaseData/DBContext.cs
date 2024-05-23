using FakeBankAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FakeBankAPI.BaseData
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //creates default accounts in the database
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    AccountNumber = "1234567890",
                    AccountType = "Savings",
                    Balance = 1000,
                    Currency = "USD",
                    Name = "John Doe",
                    InterestRate = 0.05,
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now
                },
                new Account
                {
                    Id = 2,
                    AccountNumber = "0987654321",
                    AccountType = "Checking",
                    Balance = 500,
                    Currency = "USD",
                    Name = "Jane Doe",
                    InterestRate = 0.01,
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now
                }
            );
        }
    }
}
