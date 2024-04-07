using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class DangerousMaterialMap : IEntityTypeConfiguration<DangerousMaterial>
    {
        public void Configure(EntityTypeBuilder<DangerousMaterial> builder)
        {
            builder.ToTable(@"DangerousMaterials", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
        }
    }
}
