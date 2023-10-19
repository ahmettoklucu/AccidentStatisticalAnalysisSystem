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
    public class EnvironmentalDamageMap:EntityTypeConfiguration<EnvironmentalDamage>
    {
        public void Configure(EntityTypeBuilder<EnvironmentalDamage> builder)
        {
            ToTable(@"EnvironmentalDamageS", "dbo");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.EnvironmentalDamageCategoryId).HasColumnName("EnvironmentalDamageCategoryId");
            HasRequired(x => x.EnvironmentalDamageCategory)
                .WithMany(x => x.EnvironmentalDamage)
                .HasForeignKey(x => x.EnvironmentalDamageCategoryId);
        }
    }
}
