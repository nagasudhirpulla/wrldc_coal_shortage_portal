using CoalShortagePortal.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CoalShortagePortal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<GeneratingStationForCoalShortage> GeneratingStationForCoalShortages { get; set; }
        public DbSet<GeneratingStationForCriticalCoal> GeneratingStationForCriticalCoals { get; set; }
        public DbSet<GeneratingStationForOtherReason> GeneratingStationForOtherReasons { get; set; }
        public DbSet<CoalShortageResponse> CoalShortageResponses { get; set; }
        public DbSet<OtherReasonsResponse> OtherReasonsResponses { get; set; }
        public DbSet<CriticalCoalResponse> CriticalCoalResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
