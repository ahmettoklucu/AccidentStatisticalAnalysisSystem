using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class RootCategory2:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RootCategory1Id { get; set; }
        [ForeignKey("RootCategory1Id")]
        public RootCategory1 RootCategory1 { get; set; }
        public List<RootCategory3> RootCategory3 { get; set; }
        public List<Root> Root { get; set; }
        public List<RootIncident> RootIncident { get; set; }
    }
}
