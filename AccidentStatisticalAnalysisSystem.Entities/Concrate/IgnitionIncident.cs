
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
    public class IgnitionIncident : IEntity
    {
        [Key, Column(Order = 0)]
        public Guid IncidentId { get; set; }
        [ForeignKey("IncidentId")]
        public Incident Incident { get; set; }
        [Key, Column(Order = 1)]
        public int IgnitionId { get; set; }
        [ForeignKey("IgnitionId")]
        public Ignition Ignition { get; set; }
        public bool Value { get; set; }

    }
}