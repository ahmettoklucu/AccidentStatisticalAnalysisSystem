using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResponseModel
{
    public class GetAllUserResponse
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
