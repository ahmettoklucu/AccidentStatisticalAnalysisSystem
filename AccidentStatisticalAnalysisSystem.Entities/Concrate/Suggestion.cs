
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class Suggestion : IEntity
    {
        public int Id { get; set; }
        public string GYSAspect { get; set; }
        public string BasisOfLaw { get; set; }
        public string BaseMaterial { get; set; }
        public string LawName { get; set; }
        public string LawMaterial { get; set; }
        public string LawDescription { get; set; }
        public string Other { get; set; }
        public int RootCategory3Id { get; set; }
        public int RootCategory2Id { get; set; }
        public int RootCategory1Id { get; set; }
        public List<RootIncident> RootIncident { get; set; }
    }
}