using AccidentStatisticalAnalysisSystem.DataAccess.Abstract;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Repository;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate
{
    public class EfRoleQueryable:EfQueryableRepository<Role>,IRoleQueryable
    {
        public EfRoleQueryable(AsascContext context) : base(context)
        {

        }
    }
}
