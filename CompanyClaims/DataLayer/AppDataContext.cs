using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DataLayer
{
    public class AppDataContext : DbContext
    {
        protected override void OnConfiguring
               (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CompanyClaimDb");
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }       

    }
}