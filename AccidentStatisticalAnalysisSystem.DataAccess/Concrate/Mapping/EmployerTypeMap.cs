using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class EmployerTypeMap : IEntityTypeConfiguration<EmployerType>
    {
        public void Configure(EntityTypeBuilder<EmployerType> builder)
        {
            builder.ToTable(@"EmployerTypes", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
        }
    }
}
