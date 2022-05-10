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
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13; Database=BMS_DB; Trusted_Connection=True; MultipleActiveResultSets=True;");
        }
    }
}
