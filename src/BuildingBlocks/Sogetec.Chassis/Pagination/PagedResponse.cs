namespace Sogetec.Chassis.Pagination;

public sealed class PagedResponse<T>
{
    public required IReadOnlyList<T> Items { get; init; }

    public required PagedResponseMetadata PaginationMetadata { get; init; }

    public static PagedResponse<T> Create(
        IReadOnlyList<T> items,
        int pageIndex,
        int pageSize,
        long totalItems) => new()
        {
            Items = items,
            PaginationMetadata = new PagedResponseMetadata
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems
            }
        };

}

public sealed class PagedResponseMetadata
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }

    public long TotalItems { get; init; }

    public long TotalPages => PageSize <= 0 ? 0 : (long)Math.Ceiling((double)TotalItems / PageSize);

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;
}

public static class PagedResponseExtensions
{
    extension<TIn>(PagedResponse<TIn> source)
    {
        public PagedResponse<Tout> From<Tout>(Func<TIn, Tout> mapper)
            => PagedResponse<Tout>.Create(
                    [.. source.Items.Select(mapper)],
                    source.PaginationMetadata.PageIndex,
                    source.PaginationMetadata.PageSize,
                    source.PaginationMetadata.TotalItems
                );
    }
}

