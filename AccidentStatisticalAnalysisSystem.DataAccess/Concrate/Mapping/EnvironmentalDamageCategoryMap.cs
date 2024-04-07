using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class EnvironmentalDamageCategoryMap : IEntityTypeConfiguration<EnvironmentalDamageCategory>
    {
        public void Configure(EntityTypeBuilder<EnvironmentalDamageCategory> builder)
        {
            builder.ToTable(@"EnvironmentalDamageCategories", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
        }
    }
}
