using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoalShortagePortal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoalShortagePortal.Data.Configurations
{
    public class CoalShortageResponseConfiguration : IEntityTypeConfiguration<CoalShortageResponse>
    {
        public void Configure(EntityTypeBuilder<CoalShortageResponse> builder)
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
