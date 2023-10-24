
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
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public  Company Company { get; set; }
        public string IncidentDescription { get; set; }
        public int Deaths { get; set; }
        public int Injured { get; set; }
        public int Evacuation { get; set; }
        public bool IsDelete { get; set; }
        public int CityId { get; set; }
        public bool EnvironmentalDamage { get; set; }
        public double PropertyDamagerty { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }=DateTime.Now;
        public DateTime Date { get; set; } = DateTime.Now;
        public int OperatingModesId { get; set; }
        public OperatingMode OperatingMode { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int EmployerTypeId { get; set; }

        public EmployerType EmployerType { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
        public List<FinanceIncident> FinanceIncidents { get; set; }
        public List<FormOfTheChemicalIncident> FormOfTheChemicalIncidents { get; set; }
        public List<EnvironmentalDamageIncident> EnvironmentalDamageIncidents { get; set; }
        public List<IgnitionIncident> IgnitionIncidents { get; set; }
        public List<ProcesIncident> ProcesIncident { get; set; }
        public List<DangerousMaterialIncident> DangerousMaterialIncidents { get; set; }
        public List<RootIncident> RootIncident { get; set; }
        public List<IncidentTypeIncident> IncidentTypeIncidents { get; set; }
    }
}