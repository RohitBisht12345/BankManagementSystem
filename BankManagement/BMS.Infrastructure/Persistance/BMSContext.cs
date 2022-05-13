using BMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BMS.Infrastructure.Persistance
{
    public class BmsContext : DbContext
    {
        public BmsContext(DbContextOptions<BmsContext> options) : base(options)
        {

        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Loans> Loans { get; set; }

    }
}
