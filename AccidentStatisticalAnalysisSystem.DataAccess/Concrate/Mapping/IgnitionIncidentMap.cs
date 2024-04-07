using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class IgnitionIncidentMap : IEntityTypeConfiguration<IgnitionIncident>
    {
        public void Configure(EntityTypeBuilder<IgnitionIncident> builder)
        {
            builder.ToTable("IgnitionIncidents", "dbo");
            builder.HasKey(x => new { x.IncidentId, x.IgnitionId });
            builder.Property(x => x.IncidentId).HasColumnName("IncidentId");
            builder.Property(x => x.IgnitionId).HasColumnName("IgnitionId");
            builder.Property(x => x.Value).HasColumnName("Value");

            builder.HasOne(x => x.Incident)
                .WithMany(x => x.IgnitionIncidents)
                .HasForeignKey(x => x.IncidentId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir

            builder.HasOne(x => x.Ignition)
                .WithMany(x => x.IgnitionIncidents)
                .HasForeignKey(x => x.IgnitionId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir
        }
    }
}
