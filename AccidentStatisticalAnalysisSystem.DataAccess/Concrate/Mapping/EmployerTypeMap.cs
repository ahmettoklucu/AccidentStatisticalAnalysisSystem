﻿using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping
{
    public class EmployerTypeMap : EntityTypeConfiguration<EmployerType>
    {
        public void Configure(EntityTypeBuilder<EmployerType> builder)
        {
            ToTable(@"EmployerTypes", "dbo");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
