using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class ProcesMap:EntityTypeConfiguration<Proces>
    {
        public ProcesMap()
        {
            ToTable(@"Proces", "dbo");
            HasKey(x=> x.Id);
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.ProcesCategory3Id).HasColumnName("ProcesCategory3Id");
            Property(x => x.ProcesCategory2Id).HasColumnName("ProcesCategory2Id");
            Property(x => x.ProcesCategory1Id).HasColumnName("ProcesCategory1Id");
        }
    }
}
