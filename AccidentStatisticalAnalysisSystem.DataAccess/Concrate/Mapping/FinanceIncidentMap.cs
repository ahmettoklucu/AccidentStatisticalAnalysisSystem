using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class FinanceIncidentMap:EntityTypeConfiguration<FinanceIncident>
    {
        public FinanceIncidentMap()
        {
            ToTable(@"FinanceIncidents","dbo");
            HasKey(x=> x.IncidentId);
            HasKey(x => x.FinaceId);
            Property(x => x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.FinaceId).HasColumnName("FinaceId");
            Property(x=>x.Value).HasColumnName("Value");


            HasRequired(x => x.Incident)
               .WithMany(x => x.FinanceIncidents)
               .HasForeignKey(x => x.IncidentId);
            HasRequired(x => x.Finance)
                .WithMany(x => x.FinanceIncidents)
                .HasForeignKey(x => x.FinaceId);

        }
    }
}
