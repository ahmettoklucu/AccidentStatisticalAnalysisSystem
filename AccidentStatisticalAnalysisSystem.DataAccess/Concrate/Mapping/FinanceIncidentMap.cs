using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class FinanceIncidentMap : IEntityTypeConfiguration<FinanceIncident>
    {
        public void Configure(EntityTypeBuilder<FinanceIncident> builder)
        {
            builder.ToTable("FinanceIncidents", "dbo");
            builder.HasKey(x => new { x.IncidentId, x.FinaceId });
            builder.Property(x => x.IncidentId).HasColumnName("IncidentId");
            builder.Property(x => x.FinaceId).HasColumnName("FinaceId");
            builder.Property(x => x.Value).HasColumnName("Value");

            builder.HasOne(x => x.Incident)
                .WithMany(x => x.FinanceIncidents)
                .HasForeignKey(x => x.IncidentId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir

            builder.HasOne(x => x.Finance)
                .WithMany(x => x.FinanceIncidents)
                .HasForeignKey(x => x.FinaceId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir
        }
    }
}
