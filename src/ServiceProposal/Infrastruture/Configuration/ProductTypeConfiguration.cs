

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Configuration
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductType");
            builder.HasKey(c => c.ProductTypeId);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(c => c.Description)
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
