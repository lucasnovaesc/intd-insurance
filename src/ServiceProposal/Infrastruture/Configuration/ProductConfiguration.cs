
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

            builder.HasData(
                new Product(new Guid("50beec7c-b8f6-4edb-9730-6517fdf64b2f"), "cobertura bronze", "cobertura bronze com 50 hospitais", new Guid("0fa7d783-e5b6-4317-ae7e-e932d1a8d92b"), 100, DateTime.Parse("2025-10-01T14:30:00"), DateTime.Parse("2025-10-01T14:30:00")));
        }
    }
}
