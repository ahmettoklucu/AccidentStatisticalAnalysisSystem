using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class FormOfTheChemicalIncidentMap:EntityTypeConfiguration<FormOfTheChemicalIncident>
    {
        public FormOfTheChemicalIncidentMap()
        {
            ToTable(@"FormOfTheChemicalIncidents", "dbo");
            HasKey(x => x.IncidentId);
            HasKey(x => x.FormOfTheChemicalId);
            Property(x => x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.FormOfTheChemicalId).HasColumnName("FormOfTheChemicalId");
            Property(x => x.Value).HasColumnName("Value");
        }
    }
}
