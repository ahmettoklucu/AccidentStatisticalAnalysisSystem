
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
    public class DangerousMaterialIncident : IEntity
    {
        public Guid IncidentId { get; set; }
        public Incident Incident { get; set; }

        public int DangerousMaterialId { get; set; }

        public DangerousMaterial DangerousMaterial { get; set; }
        public bool Value { get; set; }

    }
}