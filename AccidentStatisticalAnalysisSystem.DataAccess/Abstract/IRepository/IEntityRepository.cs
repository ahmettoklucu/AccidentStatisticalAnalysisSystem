using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Abstract.IRepository
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAllAsyc(Expression<Func<T, bool>> filter = null);
        Task<T> GetAsyc(Expression<Func<T, bool>> filter);
        T Get(Expression<Func<T, bool>> filter);
        Task<T> AddAsyc(T entity);
        Task<T> UpdateAsyc(T entity);
        void DeleteAsyc(T entity);
    }
}
