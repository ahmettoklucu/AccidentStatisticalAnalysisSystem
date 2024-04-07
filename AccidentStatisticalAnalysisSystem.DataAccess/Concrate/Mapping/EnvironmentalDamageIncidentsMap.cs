using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate
{
    public class EnvironmentalDamageIncidentsMap : IEntityTypeConfiguration<EnvironmentalDamageIncident>
    {
        public void Configure(EntityTypeBuilder<EnvironmentalDamageIncident> builder)
        {
            builder.ToTable("EnvironmentalDamageIncidents", "dbo");
            builder.HasKey(x => new { x.IncidentId, x.EnvironmentalDamageId });
            builder.Property(x => x.IncidentId).HasColumnName("IncidentId");
            builder.Property(x => x.EnvironmentalDamageId).HasColumnName("EnvironmentalDamageId");
            builder.Property(x => x.Value).HasColumnName("Value");
        }
    }
}
