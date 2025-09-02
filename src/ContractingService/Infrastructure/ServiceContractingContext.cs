

using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ServiceContractingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost:5432;Database=servicecontracting;User Id=postgres;Password=12345;Include Error Detail=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ServiceContractingConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ServiceContracting> ServiceContractings { get; set; }
    }
}
