using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class SuggestionMap:EntityTypeConfiguration<Suggestion>
    {
        public SuggestionMap()
        {
            ToTable(@"Suggestion", "dbo");
            HasKey(x=>x.Id);
            Property(x=>x.Id).HasColumnName("RootId");
            Property(x => x.GYSAspect).HasColumnName("GYSAspect");
            Property(x => x.BasisOfLaw).HasColumnName("BasisOfLaw");
            Property(x => x.BaseMaterial).HasColumnName("BaseMaterial");
            Property(x => x.LawName).HasColumnName("LawName");
            Property(x => x.LawMaterial).HasColumnName("LawMaterial");
            Property(x => x.LawDescription).HasColumnName("LawDescription");
            Property(x => x.Other).HasColumnName("Other");


        }
    }
}
