using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class ProcesCategory3:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcesCategory2Id { get; set; }


        public ProcesCategory2 ProcesCategory2 { get; set; }
        public List<Proces> Proces { get; set; }
        public List<ProcesIncident> ProcesIncident { get; set; }

    }
}
