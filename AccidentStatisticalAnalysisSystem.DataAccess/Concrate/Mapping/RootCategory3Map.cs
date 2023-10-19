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
    public class RootCategory3Map:EntityTypeConfiguration<RootCategory3>
    {
        public RootCategory3Map()
        {
            ToTable(@"RootCategory3", "dbo");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.RootCategory2Id).HasColumnName("RootCategory2Id");
            HasRequired(x => x.RootCategory2)
               .WithMany(x => x.RootCategory3)
               .HasForeignKey(x => x.RootCategory2Id);

        }
    }
}
