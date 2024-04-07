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
    public class ProcessCategory2Map : IEntityTypeConfiguration<ProcesCategory2>
    {
        public void Configure(EntityTypeBuilder<ProcesCategory2> builder)
        {
            builder.ToTable("ProcesCategory2", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.ProcesCategory1Id).HasColumnName("ProcesCategory1Id");

            builder.HasOne(x => x.ProcesCategory1)
                .WithMany(x => x.ProcesCategory2)
                .HasForeignKey(x => x.ProcesCategory1Id);
        }
    }
}
