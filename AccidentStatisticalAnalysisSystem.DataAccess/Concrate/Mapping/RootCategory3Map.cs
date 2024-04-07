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
    public class RootCategory3Map : IEntityTypeConfiguration<RootCategory3>
    {
        public void Configure(EntityTypeBuilder<RootCategory3> builder)
        {
            builder.ToTable("RootCategory3", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.RootCategory2Id).HasColumnName("RootCategory2Id");
            builder.HasOne(x => x.RootCategory2)
                .WithMany(x => x.RootCategory3)
                .HasForeignKey(x => x.RootCategory2Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
