namespace Application.Common.Specifications;

public abstract class PaginationSpecification<TEntity> : BaseSpecification<TEntity>, IPaginationSpecification<TEntity>
{
    private const int MinPageIndex = 1;
    private const int MinPageSize = 1;
    private const int MaxPageSize = 50;
    private const int DefaultPageIndex = 1;
    private const int DefaultPageSize = 6;
    
    public int Skip { get; }
    public int Take { get; }
    public int PageIndex { get; }
    public int PageSize { get; }

    protected PaginationSpecification(int? pageIndex, int? pageSize)
    {
        var validPageIndex = GetValidPageIndex(pageIndex);
        var validPageSize = GetValidPageSize(pageSize);
        
        Skip = validPageSize * (validPageIndex - 1);
        Take = validPageSize;

        PageIndex = validPageIndex;
        PageSize = validPageSize;
    }

    private static int GetValidPageIndex(int? pageIndex)
    {
        return pageIndex switch
        {
            null => DefaultPageIndex,
            < MinPageIndex => MinPageIndex,
            _ => pageIndex.Value
        };
    }

    private static int GetValidPageSize(int? pageSize)
    {
        return pageSize switch
        {
            null => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            < MinPageSize => MinPageSize,
            _ => pageSize.Value
        };
    }
}