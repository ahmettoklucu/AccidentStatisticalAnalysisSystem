using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class ProcessCategory2Map:EntityTypeConfiguration<ProcesCategory2>
    {
        public ProcessCategory2Map()
        {
            ToTable(@"ProcesCategory2", "dbo");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.ProcesCategory1Id).HasColumnName("ProcesCategory1Id");
            HasRequired(x => x.ProcesCategory1)
              .WithMany(x => x.ProcesCategory2)
              .HasForeignKey(x => x.ProcesCategory1Id);

        }
    }
}
