using AccidentStatisticalAnalysisSystem.DataAccess.Abstract.IRepository;
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using Microsoft.EntityFrameworkCore;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Repository
{
    public class EfQueryableRepository<T> : IQueryableRepository<T>
        where T : class, IEntity, new()
    {
        private DbContext _context;
        private DbSet<T> _entities;
        public EfQueryableRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Table
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }
    }
}
