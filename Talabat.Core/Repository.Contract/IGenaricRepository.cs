using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository.Contract
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int? id);

        #region With Specification
        Task<IReadOnlyList<T>> GettAllWithSpecAsync(ISpecifications<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec);

        Task<int> CountAsync(ISpecifications<T> spec);
        #endregion

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
