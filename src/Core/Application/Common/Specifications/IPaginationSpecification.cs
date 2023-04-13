namespace Application.Common.Specifications;

public interface IPaginationSpecification<TEntity> : ISpecification<TEntity>
{
    int Skip { get; }
    int Take { get; }
    int PageIndex { get; }
    int PageSize { get; }
}