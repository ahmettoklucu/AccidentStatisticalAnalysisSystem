using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class EnvironmentalDamageMap : IEntityTypeConfiguration<EnvironmentalDamage>
    {
        public void Configure(EntityTypeBuilder<EnvironmentalDamage> builder)
        {
            builder.ToTable("EnvironmentalDamages", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.EnvironmentalDamageCategoryId).HasColumnName("EnvironmentalDamageCategoryId");
            builder.HasOne(x => x.EnvironmentalDamageCategory)
                .WithMany(x => x.EnvironmentalDamage)
                .HasForeignKey(x => x.EnvironmentalDamageCategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir
        }
    }
}
