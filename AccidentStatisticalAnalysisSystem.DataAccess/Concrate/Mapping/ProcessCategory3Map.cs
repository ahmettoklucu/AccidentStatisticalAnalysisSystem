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
    public class ProcessCategory3Map : IEntityTypeConfiguration<ProcesCategory3>
    {
        public void Configure(EntityTypeBuilder<ProcesCategory3> builder)
        {
            builder.ToTable("ProcesCategory3", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.ProcesCategory2Id).HasColumnName("ProcesCategory2Id");

            builder.HasOne(x => x.ProcesCategory2)
                .WithMany(x => x.ProcesCategory3)
                .HasForeignKey(x => x.ProcesCategory2Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
