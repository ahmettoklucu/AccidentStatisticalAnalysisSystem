using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class DangerousMaterialIncidentsMap:EntityTypeConfiguration<DangerousMaterialIncident>
    {
        public DangerousMaterialIncidentsMap()
        {
            ToTable(@"DangerousMaterialIncidents", "dbo");
            HasKey(x => new { x.IncidentId, x.DangerousMaterialId });
            Property(x=>x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.DangerousMaterialId).HasColumnName("DangerousMaterialId");
            Property(x=>x.Value).HasColumnName("Value");
            
            HasRequired(x => x.Incident)
                .WithMany(x => x.DangerousMaterialIncidents)
                .HasForeignKey(x => x.IncidentId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.DangerousMaterial)
                .WithMany(x => x.DangerousMaterialIncidents)
                .HasForeignKey(x => x.DangerousMaterialId)
                .WillCascadeOnDelete(false);
        }
        
    }
}
