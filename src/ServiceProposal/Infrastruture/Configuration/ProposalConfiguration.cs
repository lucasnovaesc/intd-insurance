

using Domain.Entities;
using Domain.Entities.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Configuration
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.ToTable("Proposal");
            builder.HasKey(c => c.ProposalId);

            builder.HasIndex(c => c.ProposalNumber).IsUnique();

            builder.Property(c => c.ProposalNumber)
                .IsRequired();

            builder.Property(c => c.DateCreation)
                .IsRequired()
                .HasColumnType("timestamp without time zone");


            builder.Property(c => c.DateModification)
                .IsRequired()
                .HasColumnType("timestamp without time zone");

            builder.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .IsRequired();
            builder.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .IsRequired();
            builder.HasOne(c => c.ProposalStatus)
                .WithMany()
                .HasForeignKey(c => c.ProposalStatusId)
                .IsRequired();

            builder.HasData(
                new Proposal(new Guid("cd8b89cc-92b3-4a3b-9fe4-ef2f819c9a1d"), 1, new Guid("50beec7c-b8f6-4edb-9730-6517fdf64b2f"), new Guid("690c6507-1c3b-4e84-a77e-8779fbba038d"), DateTime.Parse("2025-10-01T14:30:00"), DateTime.Parse("2025-10-01T14:30:00"), ProposalStatusEnum.Analysing));
        }
    }
}
