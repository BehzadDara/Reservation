using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservation.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task Add(T entity);
        Task Update(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetAsync(Specification<T> spec);
        Task<IList<T>> ListAsync(Specification<T> spec);
        Task<IList<T>> ListAllAsync();
    }
}