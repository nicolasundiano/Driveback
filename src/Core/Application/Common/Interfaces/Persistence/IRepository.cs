using Application.Common.Specifications;
using Domain.Common;

namespace Application.Common.Interfaces.Persistence;

public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IAggregateRoot
{
    void Add(TEntity entity);

    void Delete(TEntity entity);

    Task<TEntity?> GetTrackedAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
}