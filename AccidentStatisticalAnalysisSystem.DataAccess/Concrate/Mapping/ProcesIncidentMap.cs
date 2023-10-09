﻿using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class ProcesIncidentMap:EntityTypeConfiguration<ProcesIncident>
    {
        public ProcesIncidentMap()
        {
            ToTable(@"ProcesIncidents", "dbo");
            HasKey(x => x.IncidentId);
            HasKey(x => x.ProcesId);
            Property(x => x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.ProcesId).HasColumnName("ProcesId");
            Property(x => x.Value).HasColumnName("Value");
        }
    }
}
