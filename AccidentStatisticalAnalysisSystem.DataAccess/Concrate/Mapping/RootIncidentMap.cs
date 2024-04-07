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
    public class RootIncidentMap : IEntityTypeConfiguration<RootIncident>
    {
        public void Configure(EntityTypeBuilder<RootIncident> builder)
        {
            builder.ToTable("RootIncidents", "dbo");
            builder.HasKey(x => new { x.IncidentId, x.RootId });
            builder.Property(x => x.Value).HasColumnName("Value");

            builder.HasOne(x => x.Incident)
                .WithMany(x => x.RootIncident)
                .HasForeignKey(x => x.IncidentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Root)
                .WithMany(x => x.RootIncident)
                .HasForeignKey(x => x.RootId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
