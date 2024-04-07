using AccidentStatisticalAnalysisSystem.DataAccess.Abstract.IRepository;
using AccidentStatisticalAnalysisSystem.Entities;
using AccidentStatisticalAnalysisSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Repository
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {

        public async Task<TEntity> GetAsyc(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext _context = new TContext())
            {
                return await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext _context = new TContext())
            {
                return  _context.Set<TEntity>().SingleOrDefault(filter);
            }
        }
        public async Task<List<TEntity>> GetAllAsyc(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext _context = new TContext())
            {
                return filter == null
                    ? await _context.Set<TEntity>().AsNoTracking().ToListAsync()
                    : await _context.Set<TEntity>().Where(filter).AsNoTracking().ToListAsync();
            }
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext _context = new TContext())
            {
                return filter == null
                    ?  _context.Set<TEntity>().ToList()
                    :  _context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public async void DeleteAsyc(TEntity entity)
        {
            using (TContext _context = new TContext())
            {
                var deleteEntity = _context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> AddAsyc(TEntity entity)
        {
            using (TContext _context = new TContext())
            {
                var addedEntity = _context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await _context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<TEntity> UpdateAsyc(TEntity entity)
        {
            using (TContext _context = new TContext())
            {
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
