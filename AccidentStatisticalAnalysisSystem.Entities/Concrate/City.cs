
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class City:IEntity
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public List<Company> Companies { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}