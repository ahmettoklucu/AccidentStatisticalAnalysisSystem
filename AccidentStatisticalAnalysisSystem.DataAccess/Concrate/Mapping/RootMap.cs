using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class RootMap:EntityTypeConfiguration<Root>
    {
        public RootMap()
        {
            ToTable(@"Roots", "dbo");
            HasKey(x=> x.Id);
            Property(x=> x.Id).HasColumnName("Id");
            Property(x=>x.Name).HasColumnName("Name");
            Property(x => x.RootCategory1Id).HasColumnName("RootCategory1Id");
            Property(x => x.RootCategory2Id).HasColumnName("RootCategory2Id");
            Property(x => x.RootCategory3Id).HasColumnName("RootCategory3Id");
            HasRequired(x => x.RootCategory1)
               .WithMany(x => x.Root)
               .HasForeignKey(x => x.RootCategory1Id);
            HasRequired(x => x.RootCategory2)
               .WithMany(x => x.Root)
               .HasForeignKey(x => x.RootCategory2Id);
            HasRequired(x => x.RootCategory3)
              .WithMany(x => x.Root)
              .HasForeignKey(x => x.RootCategory3Id);

        }
    }
}
