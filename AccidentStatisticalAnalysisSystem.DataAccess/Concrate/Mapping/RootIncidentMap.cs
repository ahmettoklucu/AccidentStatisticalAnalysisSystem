﻿using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class RootIncidentMap:EntityTypeConfiguration<RootIncident>
    {
        public RootIncidentMap()
        {
            ToTable(@"RootIncidents", "dbo");
            HasKey(x => new { x.IncidentId, x.RootId });
            Property(x=>x.Value).HasColumnName("Value");
            HasRequired(x => x.Incident)
             .WithMany(x => x.RootIncident)
             .HasForeignKey(x => x.IncidentId)
             .WillCascadeOnDelete(false);
            HasRequired(x => x.Root)
               .WithMany(x => x.RootIncident)
               .HasForeignKey(x => x.RootId)
               .WillCascadeOnDelete(false);
        }
    }
}
