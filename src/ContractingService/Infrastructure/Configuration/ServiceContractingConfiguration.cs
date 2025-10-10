

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ServiceContractingConfiguration : IEntityTypeConfiguration<ServiceContracting>
    {
        public void Configure(EntityTypeBuilder<ServiceContracting> builder)
        {
            builder.ToTable("ServiceContracting");
            builder.HasKey(c => c.ServiceContractingId);

            builder.Property(c => c.DateStartContract)
                .IsRequired()
                .HasColumnType("timestamp without time zone");

            //builder.HasOne(c => c.Product)
            //    .WithMany()
            //    .HasForeignKey(c => c.ProductId)
            //    .IsRequired();
            //builder.HasOne(c => c.Customer)
            //    .WithMany()
            //    .HasForeignKey(c => c.CustomerId)
            //    .IsRequired();
        }
    }
}
