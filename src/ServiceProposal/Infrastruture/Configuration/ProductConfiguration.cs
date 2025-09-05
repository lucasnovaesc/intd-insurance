
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(c => c.ProductId);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Name)
                .HasMaxLength(120)
                .IsRequired();
            builder.Property(c => c.Description)
                .IsRequired();
            builder.Property(c => c.Price)
                .IsRequired();

            builder.Property(c => c.DateCreation)
                .IsRequired()
                .HasColumnType("timestamp without time zone");


            builder.Property(c => c.DateModification)
                .IsRequired()
                .HasColumnType("timestamp without time zone");

            builder.HasOne(c => c.ProductType)
                .WithMany()
                .HasForeignKey(c => c.ProductTypeId)
                .IsRequired();
        }
    }
}
