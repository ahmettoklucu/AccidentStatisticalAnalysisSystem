using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class EnvironmentalDamage:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EnvironmentalDamageCategoryId { get; set; }
        [ForeignKey("EnvironmentalDamageCategoryId")]
        public EnvironmentalDamageCategory EnvironmentalDamageCategory { get; set; }
        public List<EnvironmentalDamageIncident> EnvironmentalDamageIncidents { get; set; }
    }

}
