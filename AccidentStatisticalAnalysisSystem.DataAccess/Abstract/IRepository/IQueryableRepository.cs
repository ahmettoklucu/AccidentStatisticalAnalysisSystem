using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Abstract.IRepository
{
    public interface IQueryableRepository<T> 
        where T : class, IEntity, new()
    {
        IQueryable<T> Table { get; }
    }
}
