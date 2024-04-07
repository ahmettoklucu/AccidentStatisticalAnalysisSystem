using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class DangerousMaterialIncidentsMap: IEntityTypeConfiguration<DangerousMaterialIncident>
    {
        public void Configure(EntityTypeBuilder<DangerousMaterialIncident> builder)
        {
            builder.ToTable(@"DangerousMaterialIncidents", "dbo");
            builder.HasKey(x => new { x.IncidentId, x.DangerousMaterialId });
            builder.Property(x=>x.IncidentId).HasColumnName("IncidentId");
            builder.Property(x => x.DangerousMaterialId).HasColumnName("DangerousMaterialId");
            builder.Property(x=>x.Value).HasColumnName("Value");
            
            builder.HasOne(x => x.Incident)
                .WithMany(x => x.DangerousMaterialIncidents)
                .HasForeignKey(x => x.IncidentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DangerousMaterial)
                .WithMany(x => x.DangerousMaterialIncidents)
                .HasForeignKey(x => x.DangerousMaterialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}
