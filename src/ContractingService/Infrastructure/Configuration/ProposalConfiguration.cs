

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
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
            builder.Property(c => c.ProductId)
                .IsRequired();
            builder.Property(c => c.CustomerId)
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
