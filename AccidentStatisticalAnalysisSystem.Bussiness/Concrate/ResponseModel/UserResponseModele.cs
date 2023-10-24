using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel
{
    public class UserResponseModele
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public int RoleId { get; set; } = 2;
        public bool IsDelete { get; set; } = false;
        public DateTime StarDate { get; set; } = DateTime.Now;
        public Guid SecretKey { get; set; }
    }
}
