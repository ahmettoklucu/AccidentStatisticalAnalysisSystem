using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable(@"Users", "dbo");
            HasKey(x=> x.Id);
            Property(x=> x.Name).HasColumnName("Name");
            Property(x => x.SureName).HasColumnName("SureName");
            Property(x => x.UserName).HasColumnName("UserName");
            Property(x => x.Password).HasColumnName("Password");
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber");
            Property(x => x.EMail).HasColumnName("EMail");
            Property(x => x.RoleId).HasColumnName("RoleId");
            Property(x => x.IsDelete).HasColumnName("IsDelete");
            Property(x => x.StarDate).HasColumnName("StarDate");
            HasRequired(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);


        }
    }
}
