using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.EntityFreamwork.Mapping
{
    public class CompanyMap:EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            ToTable(@"Companies", "dbo");
            HasKey(x => x.Id);
            Property(x=>x.Id).HasColumnName("Id");
            Property(x=>x.CompanyName).HasColumnName("CompanyName");
            Property(x=>x.CityId).HasColumnName("CityId");
            Property(x=>x.NaceId).HasColumnName("NaceId");
            Property(x => x.CompanyTypeId).HasColumnName("CompanyTypeId");
            Property(x => x.StartDate).HasColumnName("StartDate");
            Property(x => x.NumberOfWorkers).HasColumnName("NumberOfWorkers");
            Property(x => x.IsDelete).HasColumnName("IsDelete");
            Property(x=>x.UserId).HasColumnName("UserId");
            Property(x=>x.Image).HasColumnName("Image");
            Property(x=>x.Status).HasColumnName("Status");

        }
    }
}
