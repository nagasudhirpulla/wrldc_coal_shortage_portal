using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;

namespace CoalShortagePortal.Data.Configurations
{
    public class OtherReasonsResponseConfiguration : IEntityTypeConfiguration<OtherReasonsResponse>
    {
        public void Configure(EntityTypeBuilder<OtherReasonsResponse> builder)
        {
            // Default value for remarks column
            builder
            .Property(b => b.Remarks)
            .HasDefaultValue("");

            builder
            .HasIndex(b => new { b.DataDate, b.Station })
            .IsUnique();
        }
    }
}
