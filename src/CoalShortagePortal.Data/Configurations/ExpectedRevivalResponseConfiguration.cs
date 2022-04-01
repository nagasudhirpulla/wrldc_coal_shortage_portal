using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;

namespace CoalShortagePortal.Data.Configurations
{
    public class ExpectedRevivalResponseConfiguration : IEntityTypeConfiguration<ExpectedRevivalResponse>
    {
        public void Configure(EntityTypeBuilder<ExpectedRevivalResponse> builder)
        {
            builder
            .HasIndex(b => new { b.DataDate, b.RTOutageId })
            .IsUnique();
        }
    }
}
