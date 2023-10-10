
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class Proces : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int  ProcesCategory3Id { get; set; }
        public int ProcesCategory2Id { get; set; }
        public int ProcesCategory1Id { get; set; }
        public List<ProcesIncident> ProcesIncident { get; set; }
        [ForeignKey("ProcesCategory3Id")]
        public ProcesCategory3 ProcesCategory3 { get; set; }
        [ForeignKey("ProcesCategory2Id")]
        public ProcesCategory2 ProcesCategory2 { get; set; }
        [ForeignKey("ProcesCategory1Id")]
        public ProcesCategory1 ProcesCategory1 { get; set; }

    }
}