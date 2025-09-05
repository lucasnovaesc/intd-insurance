

using Domain.Entities;
using Infrastruture.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastruture
{
    public sealed class ServiceProposalContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string caminhoConfig = AppContext.BaseDirectory;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(caminhoConfig)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("ServiceProposalDb");

            optionsBuilder.UseNpgsql(connectionString);
            // Carrega appsettings.json
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory()) // pega caminho do projeto de inicialização
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //var connectionString = configuration.GetConnectionString("DefaultConnection");

            //var optionsBuilder = new DbContextOptionsBuilder<ServiceProposalContext>();
            //optionsBuilder.UseNpgsql(connectionString);

            //return new ServiceProposalContext(optionsBuilder.Options);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProposalConfiguration());
            modelBuilder.ApplyConfiguration(new ProposalStatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
    }
}
