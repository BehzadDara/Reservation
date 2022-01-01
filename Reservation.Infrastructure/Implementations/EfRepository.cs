using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Implementations;
using Reservation.Domain.Interfaces;

namespace Reservation.Infrastructure.Implementations
{
    public class EfRepository<T> : IRepository<T> where T : class , IEntity
    {
        
        private readonly DbContext _dbContext;

        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected IQueryable<T> Table => Set;
        protected IQueryable<T> TableAsNoTracking => Set.AsNoTracking();
        protected DbSet<T> Set => _dbContext.Set<T>();

        public async Task<T> GetAsync(Specification<T> spec)
        {
            var query = await ListAsync(spec);
        
            return query.FirstOrDefault();
        }

        public Task Add(T entity)
        {
            if(entity is TrackableEntity trackable)
                trackable.CreatedAtUtc = DateTime.Now;
            
            Set.Add(entity);
            return Task.CompletedTask;
        }
        
        public Task Update(T entity)
        {
            if(entity is TrackableEntity trackable)
                trackable.UpdatedAtUtc = DateTime.Now;
            
            Set.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            if(entity is TrackableEntity trackable)
                trackable.DeletedAtUtc = DateTime.Now;

            if (entity is ISoftDeletable deletable)
            {
                deletable.IsDeleted = true;
            }
            else
            {
                Set.Remove(entity);
            }

            return Task.CompletedTask;
        }

        public virtual async Task<IList<T>> ListAsync(Specification<T> spec)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
            var listQuery = await query.ToListAsync();
            var result = new List<T>();
            foreach (var entity in listQuery)
            {
                if (entity is ISoftDeletable deletable)
                {
                    if(!deletable.IsDeleted)
                        result.Add(entity);
                }
                else
                {
                    result.Add(entity);
                }
            }
            
            return result;
        }

        public  async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await Table.SingleOrDefaultAsync(x => x.Id == id);

            if (!(entity is ISoftDeletable deletable)) return entity;
            return deletable.IsDeleted ? null : entity;
        }

        public  async Task<IList<T>> ListAllAsync()
        {
            var entities = await TableAsNoTracking.ToListAsync();
            var exitEntities = new List<T>();
            foreach (var entity in entities)
            {
                if (entity is ISoftDeletable deletable)
                {
                    if (!deletable.IsDeleted)
                    {
                        exitEntities.Add(entity);
                    }
                }
                else
                {
                    exitEntities.Add(entity);
                }
            }

            return exitEntities;
        }
        
    }
}