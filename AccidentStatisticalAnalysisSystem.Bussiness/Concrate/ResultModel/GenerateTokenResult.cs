using AccidentStatisticalAnalysisSystem.Bussiness.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.Concrate.ResultModel
{
    public class GenerateTokenResult
    {
        public Token token { get; set; }
        public string Error { get; set; }
    }
}
