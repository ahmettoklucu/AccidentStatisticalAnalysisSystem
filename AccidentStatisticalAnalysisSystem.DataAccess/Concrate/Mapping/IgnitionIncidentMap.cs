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
            HasKey(x => x.IncidentId);
            HasKey(x => x.IgnitionId);
            Property(x => x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.IgnitionId).HasColumnName("IgnitionId");
            Property(x=>x.Value).HasColumnName("Value");
        }
    }
}
