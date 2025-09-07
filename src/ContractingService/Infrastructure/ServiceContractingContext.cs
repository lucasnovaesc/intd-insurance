

using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class ServiceContractingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string caminhoConfig = AppContext.BaseDirectory;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(caminhoConfig)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("PostgresConnection");

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ServiceContractingConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ServiceContracting> ServiceContractings { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
    }
}
