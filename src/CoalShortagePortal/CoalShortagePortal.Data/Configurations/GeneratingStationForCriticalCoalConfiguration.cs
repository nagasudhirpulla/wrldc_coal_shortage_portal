using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;
using System;

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

            builder
            .Property(b => b.SerialNum)
            .HasDefaultValue(1);

            // region default value
            builder
            .Property(b => b.Region)
            .HasDefaultValue(RegionName.WR);

            // region enum store as string
            builder
            .Property(b => b.Region)
            .HasConversion(
            v => v.ToString(),
            v => (RegionName)Enum.Parse(typeof(RegionName), v));
        }
    }
}
