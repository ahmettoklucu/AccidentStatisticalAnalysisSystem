
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class EnvironmentalDamageIncident : IEntity
    {

        public Guid IncidentId { get; set; }

        public Incident Incident { get; set; }
        public int EnvironmentalDamageId { get; set; }
        public bool Value { get; set; }

    }
}