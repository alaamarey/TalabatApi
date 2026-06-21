using Talabat.Core.Entities;

namespace Talabat.Core.Repository.Contract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
