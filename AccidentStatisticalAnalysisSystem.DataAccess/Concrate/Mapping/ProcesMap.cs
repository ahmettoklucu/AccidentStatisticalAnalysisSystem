using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class ProcesMap : IEntityTypeConfiguration<Proces>
    {
        public void Configure(EntityTypeBuilder<Proces> builder)
        {
            builder.ToTable("Proces", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.ProcesCategory3Id).HasColumnName("ProcesCategory3Id");
            builder.Property(x => x.ProcesCategory2Id).HasColumnName("ProcesCategory2Id");
            builder.Property(x => x.ProcesCategory1Id).HasColumnName("ProcesCategory1Id");

            builder.HasOne(x => x.ProcesCategory1)
                   .WithMany(x => x.Proces)
                   .HasForeignKey(x => x.ProcesCategory1Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProcesCategory2)
                   .WithMany(x => x.Proces)
                   .HasForeignKey(x => x.ProcesCategory2Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProcesCategory3)
                   .WithMany(x => x.Proces)
                   .HasForeignKey(x => x.ProcesCategory3Id)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
