
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
    public class IncidentTypeIncident : IEntity
    {

        public Guid IncidentId { get; set; }

        public Incident Incident { get; set; }

        public int IncidenTypeId { get; set; }

        public IncidentType IncidentType { get; set; }
        public bool Value { get; set; }
        public int IncidentTypeCategoryId { get; set; }

        public IncidentTypeCategory IncidentTypeCategory { get; set; }
    }
}