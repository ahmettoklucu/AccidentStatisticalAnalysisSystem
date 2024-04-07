using AccidentStatisticalAnalysisSystem.Entities.Concrate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class CompanyTypeMap: IEntityTypeConfiguration<CompanyType>
    {
        public void Configure(EntityTypeBuilder<CompanyType> builder)
        {
            builder.ToTable(@"CompanyTypes", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            
        }
    }
}
