
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class Root : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RootCategory3Id { get; set; }
        public int RootCategory2Id { get; set; }
        public int RootCategory1Id { get; set; }

        public RootCategory3 RootCategory3 { get; set; }

        public RootCategory2 RootCategory2 { get; set; }

        public RootCategory1 RootCategory1 { get; set; }
        public List<RootIncident> RootIncident { get; set; }
    }
}