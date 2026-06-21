using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repository.Contract;
using Talabat.repository.Data;

namespace Talabat.repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalabatDbContext _talabatDbContext;
        private Dictionary<string,object> _repositories;
        public UnitOfWork(TalabatDbContext talabatDbContext) //  Ask CLR to create object from Context
        {
            _talabatDbContext = talabatDbContext;
            _repositories = new Dictionary<string, object>();
        }



        public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key)) 
            {
                var value =  new GenaricRepository<TEntity>(_talabatDbContext);
                _repositories.Add(key, value);
            }
            return  _repositories[key] as IGenaricRepository<TEntity>;
        }


        public async ValueTask DisposeAsync()
        => await _talabatDbContext.DisposeAsync();

        public async Task<int> CompleteAsync()
            => await _talabatDbContext.SaveChangesAsync();
    }
}
