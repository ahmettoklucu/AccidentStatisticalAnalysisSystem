using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class IncidentTypeIncidentMap:EntityTypeConfiguration<IncidentTypeIncident>
    {
        public IncidentTypeIncidentMap()
        {
            ToTable(@"IncidentTypeIncidents", "dbo");
            HasKey(x => new { x.IncidentId, x.IncidenTypeId });
            Property(x=>x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.IncidenTypeId).HasColumnName("IncidenTypeId");
            Property(x => x.IncidentTypeCategoryId).HasColumnName("IncidentTypeCategoryId");

            HasRequired(x => x.Incident)
              .WithMany(x => x.IncidentTypeIncidents)
              .HasForeignKey(x => x.IncidentId);

            HasRequired(x => x.IncidentType)
               .WithMany(x => x.IncidentTypeIncidents)
               .HasForeignKey(x => x.IncidenTypeId);

            HasRequired(x => x.IncidentTypeCategory)
              .WithMany(x => x.IncidentTypeIncidents)
              .HasForeignKey(x => x.IncidentTypeCategoryId);

        }
    }
}
