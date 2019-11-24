using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;

namespace CoalShortagePortal.Data.Configurations
{
    public class GeneratingStationForCriticalCoalConfiguration : IEntityTypeConfiguration<GeneratingStationForCriticalCoal>
    {
        public void Configure(EntityTypeBuilder<GeneratingStationForCriticalCoal> builder)
        {
            builder
            .HasIndex(b => new { b.StartDate, b.Name })
            .IsUnique();

            builder
            .HasIndex(b => new { b.EndDate, b.Name })
            .IsUnique();
        }
    }
}
