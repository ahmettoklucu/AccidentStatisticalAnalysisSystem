
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
        [Key, Column(Order = 0)]
        public Guid IncidentId { get; set; }
        [ForeignKey("IncidentId")]
        public Incident Incident { get; set; }
        [Key, Column(Order = 1)]
        public int IncidenTypeId { get; set; }
        [ForeignKey("IncidentId")]
        public IncidentType IncidentType { get; set; }
        public bool Value { get; set; }
        public int IncidentTypeCategoryId { get; set; }
        [ForeignKey("IncidentTypeCategoryId")]
        public IncidentTypeCategory IncidentTypeCategory { get; set; }
    }
}