using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;

namespace CoalShortagePortal.Data.Configurations
{
    public class CriticalCoalResponseConfiguration : IEntityTypeConfiguration<CriticalCoalResponse>
    {
        public void Configure(EntityTypeBuilder<CriticalCoalResponse> builder)
        {
            builder
            .HasIndex(b => new { b.DataDate, b.Station })
            .IsUnique();
        }
    }
}
