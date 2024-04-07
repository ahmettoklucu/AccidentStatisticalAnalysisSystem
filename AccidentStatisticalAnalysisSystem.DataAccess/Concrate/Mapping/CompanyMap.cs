using AccidentStatisticalAnalysisSystem.Entities.Concrate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.EntityFreamwork.Mapping
{
    public class CompanyMap: IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(@"Companies", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).HasColumnName("Id");
            builder.Property(x=>x.CompanyName).HasColumnName("CompanyName");
            builder.Property(x=>x.CityId).HasColumnName("CityId");
            builder.Property(x=>x.NaceId).HasColumnName("NaceId");
            builder.Property(x => x.CompanyTypeId).HasColumnName("CompanyTypeId");
            builder.Property(x => x.StartDate).HasColumnName("StartDate");
            builder.Property(x => x.NumberOfWorkers).HasColumnName("NumberOfWorkers");
            builder.Property(x => x.IsDelete).HasColumnName("IsDelete");
            builder.Property(x=>x.UserId).HasColumnName("UserId");
            builder.Property(x=>x.Image).HasColumnName("Image");
            builder.Property(x=>x.Status).HasColumnName("Status");
            builder.HasOne(x => x.User)
                .WithMany(x => x.Companies)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.CompanyType)
                .WithMany(x => x.Companies)
                .HasForeignKey(x => x.CompanyTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Nace)
               .WithMany(x => x.Companies)
               .HasForeignKey(x => x.NaceId)
               .OnDelete(DeleteBehavior.Restrict);
             
        }
    }
}
