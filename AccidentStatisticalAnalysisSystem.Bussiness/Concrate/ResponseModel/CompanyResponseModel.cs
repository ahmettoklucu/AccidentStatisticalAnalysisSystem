using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel
{
    public class CompanyResponseModel
    {
        public Guid Id { get; set; } = new Guid();
        public string CompanyName { get; set; }
        public int CityId { get; set; }
        public int NaceId { get; set; }
        public Nace Nace { get; set; }
        public int CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public int NumberOfWorkers { get; set; }
        public bool IsDelete { get; set; }
        public Guid UserId { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
    }
}
