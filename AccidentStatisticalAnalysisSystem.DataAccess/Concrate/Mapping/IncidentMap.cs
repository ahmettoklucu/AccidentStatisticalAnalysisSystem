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
    public class IncidentMap : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.ToTable("Incidents", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.CompanyId).HasColumnName("CompanyId");
            builder.Property(x => x.IncidentDescription).HasColumnName("IncidentDescription");
            builder.Property(x => x.Deaths).HasColumnName("Deaths");
            builder.Property(x => x.Injured).HasColumnName("Injured");
            builder.Property(x => x.Evacuation).HasColumnName("Evacuation");
            builder.Property(x => x.IsDelete).HasColumnName("IsDelete");
            builder.Property(x => x.CityId).HasColumnName("CityId");
            builder.Property(x => x.EnvironmentalDamage).HasColumnName("EnvironmentalDamage");
            builder.Property(x => x.PropertyDamagerty).HasColumnName("PropertyDamagerty");
            builder.Property(x => x.StartDate).HasColumnName("StartDate");
            builder.Property(x => x.Date).HasColumnName("Date");
            builder.Property(x => x.OperatingModesId).HasColumnName("OperatingModesId");
            builder.Property(x => x.NotificationId).HasColumnName("NotificationId");
            builder.Property(x => x.Hour).HasColumnName("Hour");
            builder.Property(x => x.Minute).HasColumnName("Minute");
            builder.Property(x => x.EmployerTypeId).HasColumnName("EmployerTypeId");
            builder.Property(x => x.Status).HasColumnName("Status");
            builder.Property(x => x.Image).HasColumnName("Image");

            builder.HasOne(x => x.Notification)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir

            builder.HasOne(x => x.OperatingMode)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.OperatingModesId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir

            builder.HasOne(x => x.EmployerType)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.EmployerTypeId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict); // Ya da Cascade, SetNull, vs. olabilir
        }
    }
}
