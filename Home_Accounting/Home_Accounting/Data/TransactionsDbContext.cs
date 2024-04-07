using Home_Accounting.Models;
using HomeAccounting.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Home_Accounting.Data
{
    public class TransactionsDbContext : IdentityDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryType> Types { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options)
            : base(options)
        {
        }
      
    }
}