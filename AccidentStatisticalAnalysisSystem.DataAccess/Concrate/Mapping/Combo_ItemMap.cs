using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class Combo_ItemMap:EntityTypeConfiguration<Combo_Item>
    {
        public Combo_ItemMap()
        {
            ToTable(@"Combo_Item","dbo");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x=>x.Type).HasColumnName("Type");
            Property(x=>x.Name).HasColumnName("Name");
        }
    }
}
