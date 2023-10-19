﻿
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class FormOfTheChemicalIncident : IEntity
    {
        public Guid IncidentId { get; set; }
        [ForeignKey("IncidentId")]
        public Incident Incident { get; set; }
        public int FormOfTheChemicalId { get; set; }
        [ForeignKey("FormOfTheChemicalId")]
        public FormOfTheChemical FormOfTheChemical { get; set; }
        public bool Value { get; set; }

    }
}