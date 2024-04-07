using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class IgnitionMap : IEntityTypeConfiguration<Ignition>
    {
        public void Configure(EntityTypeBuilder<Ignition> builder)
        {
            builder.ToTable("Ignitions", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
        }
    }
}
