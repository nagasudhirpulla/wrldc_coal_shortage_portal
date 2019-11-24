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
            builder
            .HasIndex(b => new { b.DataDate, b.Station })
            .IsUnique();
        }
    }
}
