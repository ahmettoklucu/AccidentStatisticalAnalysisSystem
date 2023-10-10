﻿using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class RootCategory3:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RootCategory2Id { get; set; }
        [ForeignKey("RootCategory2Id")]
        public RootCategory2 RootCategory2 { get; set; }
        public List<Root> Roots { get; set; }
        public List<RootIncident> RootIncident { get; set; }
    }
}