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
 
        public class IncidentTypeMap : IEntityTypeConfiguration<IncidentType>
        {
            public void Configure(EntityTypeBuilder<IncidentType> builder)
            {
                builder.ToTable("IncidentTypes", "dbo");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id");
                builder.Property(x => x.Name).HasColumnName("Name");
                builder.Property(x => x.IncidentTypeCategoryId).HasColumnName("IncidentTypeCategoryId");

                builder.HasOne(x => x.IncidentTypeCategory)
                    .WithMany(x => x.IncidentTypes)
                    .HasForeignKey(x => x.IncidentTypeCategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    
}
