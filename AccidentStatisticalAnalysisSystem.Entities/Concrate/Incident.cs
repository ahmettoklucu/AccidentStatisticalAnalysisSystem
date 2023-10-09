
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccidentStatisticalAnalysisSystem.Entities.Concrate
{
    public class Incident : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public  Company Company { get; set; }
        public string IncidentDescription { get; set; }
        public int Deaths { get; set; }
        public int Injured { get; set; }
        public int Evacuation { get; set; }
        public bool IsDelete { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public bool EnvironmentalDamage { get; set; }
        public int PropertyDamagerty { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int OperatingModesId { get; set; }
        public int NotificationId { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int EmployerTypeId { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
        public List<FinanceIncident> FinanceIncidents { get; set; }
        public List<FormOfTheChemicalIncident> FormOfTheChemicalIncidents { get; set; }
        public List<EnvironmentalDamageIncident> EnvironmentalDamageIncidents { get; set; }
        public List<IgnitionIncident> IgnitionIncidents { get; set; }
        public List<ProcesIncident> ProcesIncident { get; set; }
        public List<DangerousMaterialIncident> DangerousMaterialIncidents { get; set; }
        public List<RootIncident> RootIncident { get; set; }
    }
}