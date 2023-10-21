using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class IncidentMap:EntityTypeConfiguration<Incident>
    {
        public IncidentMap()
        {
            ToTable(@"Incidents", "dbo");
            HasKey(x => x.Id);
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.CompanyId).HasColumnName("CompanyId");
            Property(x => x.IncidentDescription).HasColumnName("IncidentDescription");
            Property(x => x.Deaths).HasColumnName("Deaths");
            Property(x => x.Injured).HasColumnName("Injured");
            Property(x => x.Evacuation).HasColumnName("Evacuation");
            Property(x => x.IsDelete).HasColumnName("IsDelete");
            Property(x => x.CityId).HasColumnName("CityId");
            Property(x => x.EnvironmentalDamage).HasColumnName("EnvironmentalDamage");
            Property(x => x.PropertyDamagerty).HasColumnName("PropertyDamagerty");
            Property(x => x.StartDate).HasColumnName("StartDate");
            Property(x => x.Date).HasColumnName("Date");
            Property(x => x.OperatingModesId).HasColumnName("OperatingModesId");
            Property(x => x.NotificationId).HasColumnName("NotificationId");
            Property(x => x.Hour).HasColumnName("Hour");
            Property(x => x.Minute).HasColumnName("Minute");
            Property(x => x.EmployerTypeId).HasColumnName("EmployerTypeId");
            Property(x => x.Status).HasColumnName("Status");
            Property(x => x.Image).HasColumnName("Image");

            HasRequired(x => x.Notification)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.NotificationId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.OperatingMode)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.OperatingModesId)
                .WillCascadeOnDelete(false);
            HasRequired(x => x.EmployerType)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.EmployerTypeId)
                .WillCascadeOnDelete(false);
            HasRequired(x => x.Company)
                .WithMany(x => x.Incidents)
                .HasForeignKey(x => x.CompanyId)
                .WillCascadeOnDelete(false);

        }
    }
}
