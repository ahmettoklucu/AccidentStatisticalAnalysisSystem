using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel
{
    public class TokenPayload
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime ValidityDatetime { get; set; }
    }
}
