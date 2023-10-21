
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class FinanceIncident : IEntity
    {

        public Guid IncidentId { get; set; }

        public Incident Incident { get; set; }
        public int FinaceId { get; set; }

        public Finance Finance { get; set; }
        public double Value { get; set; }

    }
}