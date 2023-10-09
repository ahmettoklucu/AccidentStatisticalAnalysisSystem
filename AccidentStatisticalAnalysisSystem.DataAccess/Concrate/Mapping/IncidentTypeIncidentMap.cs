﻿using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class IncidentTypeIncidentMap:EntityTypeConfiguration<IncidentTypeIncident>
    {
        public IncidentTypeIncidentMap()
        {
            ToTable(@"IncidentTypeIncidents", "dbo");
            HasKey(x => x.IncidentId);
            HasKey(x => x.IncidenTypeId);
            Property(x=>x.IncidentId).HasColumnName("IncidentId");
            Property(x => x.IncidenTypeId).HasColumnName("IncidenTypeId");
            Property(x => x.IncidentTypeCategoryId).HasColumnName("IncidentTypeCategoryId");

        }
    }
}