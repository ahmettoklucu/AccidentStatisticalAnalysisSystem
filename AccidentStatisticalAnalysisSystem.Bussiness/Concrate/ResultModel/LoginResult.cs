using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel
{
    public class LoginResult
    {

            public bool Success { get; set; }
            public string Message { get; set; }
            public Token Token { get; set; }
        
    }
}
