using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class ProcesIncidentMap:EntityTypeConfiguration<ProcesIncident>
    {
        public ProcesIncidentMap()
        {
            ToTable(@"ProcesIncidents", "dbo");
            HasKey(x => new { x.IncidentId, x.ProcesId});
            Property(x => x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.ProcesId).HasColumnName("ProcesId");
            Property(x => x.Value).HasColumnName("Value");
            HasRequired(x => x.Incident)
             .WithMany(x => x.ProcesIncident)
             .HasForeignKey(x => x.IncidentId);
            HasRequired(x => x.Proces)
               .WithMany(x => x.ProcesIncident)
               .HasForeignKey(x => x.ProcesId);
        }
    }
}
