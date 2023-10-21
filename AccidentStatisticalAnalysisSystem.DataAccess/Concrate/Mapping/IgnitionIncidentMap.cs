using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class IgnitionIncidentMap:EntityTypeConfiguration<IgnitionIncident>
    {
        public IgnitionIncidentMap()
        {
            ToTable(@"IgnitionIncidents", "dbo");
            HasKey(x => new { x.IncidentId, x.IgnitionId });
            Property(x => x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.IgnitionId).HasColumnName("IgnitionId");
            Property(x=>x.Value).HasColumnName("Value");

            HasRequired(x => x.Incident)
              .WithMany(x => x.IgnitionIncidents)
              .HasForeignKey(x => x.IncidentId)
              .WillCascadeOnDelete(false);
            HasRequired(x => x.Ignition)
                .WithMany(x => x.IgnitionIncidents)
                .HasForeignKey(x => x.IgnitionId)
                .WillCascadeOnDelete(false);
        }
    }
}
