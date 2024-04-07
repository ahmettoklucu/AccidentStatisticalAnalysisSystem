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
    public class FormOfTheChemicalIncidentMap : IEntityTypeConfiguration<FormOfTheChemicalIncident>
    {
        public void Configure(EntityTypeBuilder<FormOfTheChemicalIncident> builder)
        {
            builder.ToTable("FormOfTheChemicalIncidents", "dbo");
            builder.HasKey(x => new { x.IncidentId, x.FormOfTheChemicalId });
            builder.Property(x => x.IncidentId).HasColumnName("IncidentId");
            builder.Property(x => x.FormOfTheChemicalId).HasColumnName("FormOfTheChemicalId");
            builder.Property(x => x.Value).HasColumnName("Value");

            builder.HasOne(x => x.Incident)
                .WithMany(x => x.FormOfTheChemicalIncidents)
                .HasForeignKey(x => x.IncidentId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir

            builder.HasOne(x => x.FormOfTheChemical)
                .WithMany(x => x.FormOfTheChemicalIncidents)
                .HasForeignKey(x => x.FormOfTheChemicalId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir
        }
    }
}
