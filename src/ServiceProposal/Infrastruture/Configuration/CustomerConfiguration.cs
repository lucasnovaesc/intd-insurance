

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.CustomerId);

            builder.HasIndex(c => c.Email).IsUnique();
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Name)
                .HasMaxLength(120)
                .IsRequired();
            builder.Property(c => c.PhoneNumber)
                .IsRequired();
            builder.Property(c => c.Email)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(c => c.Address)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.DateCreation)
                .IsRequired()
                .HasColumnType("timestamp without time zone");


            builder.Property(c => c.DateModification)
                .IsRequired()
                .HasColumnType("timestamp without time zone");
        }
    }
}
