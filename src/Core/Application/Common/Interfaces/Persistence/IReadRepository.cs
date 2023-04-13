using Application.Common.Specifications;
using Domain.Common;

namespace Application.Common.Interfaces.Persistence;

public interface IReadRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);

    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);
    Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
}