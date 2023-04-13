namespace Application.Common.Models;

public class PaginatedList<TEntity>
{
    public IEnumerable<TEntity> Data { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedList(
        IEnumerable<TEntity> data,
        int pageIndex,
        int pageSize,
        int totalCount)
	{
        Data = data;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
	}
}