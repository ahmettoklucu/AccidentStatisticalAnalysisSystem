using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class RootCategory1:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RootCategory2> RootCategory2 { get; set; }
        public List<Root> Root { get; set; }
        public List<RootIncident> RootIncident { get; set; }
    }
}
