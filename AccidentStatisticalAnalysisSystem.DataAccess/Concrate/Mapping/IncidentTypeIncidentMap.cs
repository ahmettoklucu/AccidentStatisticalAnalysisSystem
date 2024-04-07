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
    public class IncidentTypeIncidentMap : IEntityTypeConfiguration<IncidentTypeIncident>
    {
        public void Configure(EntityTypeBuilder<IncidentTypeIncident> builder)
        {
            builder.ToTable("IncidentTypeIncidents", "dbo");
            builder.HasKey(x => new { x.IncidentId, x.IncidenTypeId });
            builder.Property(x => x.IncidentId).HasColumnName("IncidentId");
            builder.Property(x => x.IncidenTypeId).HasColumnName("IncidenTypeId");
            builder.Property(x => x.IncidentTypeCategoryId).HasColumnName("IncidentTypeCategoryId");

            builder.HasOne(x => x.Incident)
                .WithMany(x => x.IncidentTypeIncidents)
                .HasForeignKey(x => x.IncidentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.IncidentType)
                .WithMany(x => x.IncidentTypeIncidents)
                .HasForeignKey(x => x.IncidenTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.IncidentTypeCategory)
                .WithMany(x => x.IncidentTypeIncidents)
                .HasForeignKey(x => x.IncidentTypeCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
