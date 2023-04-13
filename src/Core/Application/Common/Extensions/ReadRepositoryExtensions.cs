using Application.Common.Interfaces.Persistence;
using Application.Common.Models;
using Application.Common.Specifications;
using Domain.Common;

namespace Application.Common.Extensions;

public static class ReadRepositoryExtensions
{
    public static async Task<PaginatedList<TEntity>> PaginatedListAsync<TEntity>(
        this IReadRepository<TEntity> repository,
        IPaginationSpecification<TEntity> spec,
        CancellationToken cancellationToken = default) where TEntity: class, IEntity
    {
        var data = await repository.ListAsync(spec, cancellationToken);
        var totalCount = await repository.CountAsync(spec, cancellationToken);

        return new PaginatedList<TEntity>(
            data,
            spec.PageIndex,
            spec.PageSize,
            totalCount);
    }
}