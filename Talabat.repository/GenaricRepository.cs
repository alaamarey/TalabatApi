using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications;
using Talabat.repository.Data;

namespace Talabat.repository
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly TalabatDbContext _talabatDbContext;

        public GenaricRepository(TalabatDbContext talabatDbContext)
        {
            _talabatDbContext = talabatDbContext;
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _talabatDbContext.Set<T>().ToListAsync();



        public async Task<T?> GetAsync(int? id)
        {
            if (typeof(T) == typeof(Product))
                return _talabatDbContext.Set<Product>().Where(P => P.Id == id).OrderBy(P => P.Name)
            .Include(P => P.ProductBrand).Include(P => P.ProductCategory).FirstOrDefault() as T;
            return await _talabatDbContext.Set<T>().FindAsync(id);
        }


        public async Task<IReadOnlyList<T>> GettAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }


        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
            => SpecificationEvaluator<T>.GetQuery(_talabatDbContext.Set<T>(), spec);

        public async Task AddAsync(T entity)
        => await _talabatDbContext.AddAsync(entity);

        public void  Update(T entity)
          => _talabatDbContext.Update(entity);

        public void Delete(T entity)
        => _talabatDbContext.Remove(entity);
    }
}

