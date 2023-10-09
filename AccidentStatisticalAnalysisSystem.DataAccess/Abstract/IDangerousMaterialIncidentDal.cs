using AccidentStatisticalAnalysisSystem.DataAccess;
using AccidentStatisticalAnalysisSystem.DataAccess.Abstract.IRepository;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Abstract
{
    public interface IDangerousMaterialIncidentDal : IEntityRepository<DangerousMaterialIncident>
    {
    }
}
