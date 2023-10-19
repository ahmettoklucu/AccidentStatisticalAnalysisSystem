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
    public class RootCategory2Map:EntityTypeConfiguration<RootCategory2>
    {
        public RootCategory2Map()
        {
            ToTable(@"RootCategory2", "dbo");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.RootCategory1Id).HasColumnName("RootCategory1Id");
            HasRequired(x => x.RootCategory1)
              .WithMany(x => x.RootCategory2)
              .HasForeignKey(x => x.RootCategory1Id);

        }
    }
}
