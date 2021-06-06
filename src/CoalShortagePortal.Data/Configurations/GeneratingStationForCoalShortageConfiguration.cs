using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;
using System;

namespace CoalShortagePortal.Data.Configurations
{
    public class GeneratingStationForCoalShortageConfiguration : IEntityTypeConfiguration<GeneratingStationForCoalShortage>
    {
        public void Configure(EntityTypeBuilder<GeneratingStationForCoalShortage> builder)
        {
            // Name, StartDate unique
            builder
            .HasIndex(b => new { b.StartDate, b.Name })
            .IsUnique();

            // Name, EndDate unique
            builder
            .HasIndex(b => new { b.EndDate, b.Name })
            .IsUnique();

            // SerialNum default value
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
