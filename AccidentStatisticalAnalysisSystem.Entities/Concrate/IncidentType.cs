using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class IncidentType:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IncidentTypeCategoryId { get; set; }
        public IncidentTypeCategory IncidentTypeCategory { get; set; }
        public List<Incident> Incidents { get; set; }
        public List<IncidentTypeIncident> IncidentTypeIncidents { get; set; }

    }
}
