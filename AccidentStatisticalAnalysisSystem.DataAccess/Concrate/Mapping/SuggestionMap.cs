using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class SuggestionMap : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.ToTable("Suggestion", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("RootId");
            builder.Property(x => x.GYSAspect).HasColumnName("GYSAspect");
            builder.Property(x => x.BasisOfLaw).HasColumnName("BasisOfLaw");
            builder.Property(x => x.BaseMaterial).HasColumnName("BaseMaterial");
            builder.Property(x => x.LawName).HasColumnName("LawName");
            builder.Property(x => x.LawMaterial).HasColumnName("LawMaterial");
            builder.Property(x => x.LawDescription).HasColumnName("LawDescription");
            builder.Property(x => x.Other).HasColumnName("Other");
        }
    }
}
