using BMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMS.Infrastructure.Persistance
{
    public class BmsContext : DbContext
    {
        public BmsContext(DbContextOptions<BmsContext> options) : base(options)
        {

        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Loans> Loans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=bankdb,1433;Database=BankDb;User Id=SA;Password=SwN12345678;");
        }
    }
}
