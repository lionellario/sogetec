namespace Sogetec.Chassis.Pagination;

public record PaginationQueryFilter(int? pageIndex = null, int? pageSize = null)
{
    public readonly int PageIndex = Math.Max(1, pageIndex ?? 0);
    public readonly int PageSize = Math.Clamp(pageSize ?? 50, 1, 100);
    public int Skip => (PageIndex - 1) * PageSize;
    public int Take => PageSize;
}

public record DateQueryFilter(int? pageNumber = null, int? pageSize = null, DateTimeOffset? fromDate = null, DateTimeOffset? toDate = null) : PaginationQueryFilter(pageNumber, pageSize)
{
    public const string FROM_KEY = "fromDate";
    public const string TO_KEY = "toDate";

    public readonly DateTimeOffset? FromDate = fromDate;
    public readonly DateTimeOffset? ToDate = toDate;
}
