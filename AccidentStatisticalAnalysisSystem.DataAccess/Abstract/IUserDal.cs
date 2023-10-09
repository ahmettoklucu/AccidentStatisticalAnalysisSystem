using AccidentStatisticalAnalysisSystem.DataAccess;
using AccidentStatisticalAnalysisSystem.DataAccess.Abstract.IRepository;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
    }
}
