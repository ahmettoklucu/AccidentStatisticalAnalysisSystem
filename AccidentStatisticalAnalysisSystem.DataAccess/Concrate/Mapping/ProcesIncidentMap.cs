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
    public class ProcesIncidentMap : IEntityTypeConfiguration<ProcesIncident>
    {
        public void Configure(EntityTypeBuilder<ProcesIncident> builder)
        {
            builder.ToTable("ProcesIncidents", "dbo");
            builder.HasKey(x => new { x.IncidentId, x.ProcesId });
            builder.Property(x => x.IncidentId).HasColumnName("IncidentId");
            builder.Property(x => x.ProcesId).HasColumnName("ProcesId");
            builder.Property(x => x.Value).HasColumnName("Value");

            builder.HasOne(x => x.Incident)
                   .WithMany(x => x.ProcesIncident)
                   .HasForeignKey(x => x.IncidentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Proces)
                   .WithMany(x => x.ProcesIncident)
                   .HasForeignKey(x => x.ProcesId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
