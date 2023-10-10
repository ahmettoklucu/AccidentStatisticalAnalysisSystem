using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class FormOfTheChemical:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FormOfTheChemicalIncident> FormOfTheChemicalIncidents { get; set; }
    }
}
