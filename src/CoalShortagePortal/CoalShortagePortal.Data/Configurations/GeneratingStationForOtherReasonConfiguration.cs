using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;

namespace CoalShortagePortal.Data.Configurations
{
    public class GeneratingStationForOtherReasonConfiguration : IEntityTypeConfiguration<GeneratingStationForOtherReason>
    {
        public void Configure(EntityTypeBuilder<GeneratingStationForOtherReason> builder)
        {
            builder
            .HasIndex(b => new { b.StartDate, b.Name })
            .IsUnique();

            builder
            .HasIndex(b => new { b.EndDate, b.Name })
            .IsUnique();

            builder
            .Property(b => b.SerialNum)
            .HasDefaultValue(1);
        }
    }
}
