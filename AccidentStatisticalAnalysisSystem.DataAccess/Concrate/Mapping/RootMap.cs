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
    public class RootMap : IEntityTypeConfiguration<Root>
    {
        public void Configure(EntityTypeBuilder<Root> builder)
        {
            builder.ToTable("Roots", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.RootCategory1Id).HasColumnName("RootCategory1Id");
            builder.Property(x => x.RootCategory2Id).HasColumnName("RootCategory2Id");
            builder.Property(x => x.RootCategory3Id).HasColumnName("RootCategory3Id");

            builder.HasOne(x => x.RootCategory1)
                .WithMany(x => x.Root)
                .HasForeignKey(x => x.RootCategory1Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RootCategory2)
                .WithMany(x => x.Root)
                .HasForeignKey(x => x.RootCategory2Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RootCategory3)
                .WithMany(x => x.Root)
                .HasForeignKey(x => x.RootCategory3Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
