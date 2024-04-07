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
    public class RootCategory2Map : IEntityTypeConfiguration<RootCategory2>
    {
        public void Configure(EntityTypeBuilder<RootCategory2> builder)
        {
            builder.ToTable("RootCategory2", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.RootCategory1Id).HasColumnName("RootCategory1Id");
            builder.HasOne(x => x.RootCategory1)
                .WithMany(x => x.RootCategory2)
                .HasForeignKey(x => x.RootCategory1Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
