

using Domain.Entities;
using Domain.Entities.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Configuration
{
    public class ProposalStatusConfiguration : IEntityTypeConfiguration<ProposalStatus>
    {
        public void Configure(EntityTypeBuilder<ProposalStatus> builder)
        {
            builder.ToTable("ProposalStatus");

            builder.HasKey(s => s.StatusSystemId);

            builder.HasIndex(s => s.Description)
                .IsUnique();

            builder.Property(s => s.DateCreation)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

            builder.Property(s => s.DateModification)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

            builder.Property(s => s.Description)
                .IsRequired();

            builder.HasData(
                new ProposalStatus(ProposalStatusEnum.Analysing, "Analysing", DateTime.Parse("2025-10-01T14:30:00"), DateTime.Parse("2025-10-01T14:30:00")),
                new ProposalStatus(ProposalStatusEnum.Rejected, "Rejected", DateTime.Parse("2025-10-01T14:30:00"), DateTime.Parse("2025-10-01T14:30:00")),
                new ProposalStatus(ProposalStatusEnum.Approved, "Approved", DateTime.Parse("2025-10-01T14:30:00"), DateTime.Parse("2025-10-01T14:30:00")));
        }
    }
}
