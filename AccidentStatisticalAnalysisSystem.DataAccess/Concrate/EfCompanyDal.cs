using AccidentStatisticalAnalysisSystem.DataAccess.Abstract;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Repository;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate
{
   
}
 public class EfCompanyDal: EfEntityRepositoryBase<Company, AsascContext>,ICompanyDal
    {
    }