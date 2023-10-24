using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel
{
    public class IncidentResponseModele
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public string IncidentDescription { get; set; }
        public int Deaths { get; set; }
        public int Injured { get; set; }
        public int Evacuation { get; set; }
        public bool IsDelete { get; set; }
        public int CityId { get; set; }
        public bool EnvironmentalDamage { get; set; }
        public double PropertyDamagerty { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime Date { get; set; } = DateTime.Now;
        public int OperatingModesId { get; set; }
        public int NotificationId { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int EmployerTypeId { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
    }
}
