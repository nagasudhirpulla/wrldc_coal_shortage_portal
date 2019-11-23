using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;

namespace CoalShortagePortal.Data.Configurations
{
    public class CriticalCoalResponseConfiguration : IEntityTypeConfiguration<CriticalCoalResponse>
    {
        public void Configure(EntityTypeBuilder<CriticalCoalResponse> builder)
        {
            // Default value for remarks column
            builder
            .Property(b => b.Remarks)
            .HasDefaultValue("");
        }
    }
}
