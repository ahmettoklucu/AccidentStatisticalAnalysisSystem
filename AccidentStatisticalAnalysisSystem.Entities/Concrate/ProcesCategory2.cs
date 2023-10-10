using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class ProcesCategory2:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcesCategory1Id { get; set; }
        [ForeignKey("ProcesCategory1Id")]
        public ProcesCategory1 ProcesCategory1 { get; set; }
        public List<ProcesCategory3> ProcesCategory3 { get; set; }
        public List<Proces> Proces { get; set; }
        public List<ProcesIncident> ProcesIncident { get; set; }
    }
}
}
