using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate
{ 
    public class EnvironmentalDamageIncidentsMap:EntityTypeConfiguration<EnvironmentalDamageIncident>
    {
        public EnvironmentalDamageIncidentsMap()
        {
            ToTable(@"EnvironmentalDamageIncidents","dbo");
            HasKey(x => x.IncidentId);
            HasKey(x => x.EnvironmentalDamageId);
            Property(x => x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.EnvironmentalDamageId).HasColumnName("EnvironmentalDamageId");
            Property(x=>x.Value).HasColumnName("Value");
        }

    }
}
