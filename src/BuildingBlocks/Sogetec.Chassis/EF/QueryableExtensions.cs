using Sogetec.Chassis.Pagination;

namespace Sogetec.Chassis.EF;

public static class QueryableExtensions
{
    extension<T>(IQueryable<T> query)
    {
        public async Task<PagedResponse<T>> ToPagedResponseAsync(
        PaginationQueryFilter filter,
        CancellationToken cancellationToken = default)
        {
            long totalItems = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip(filter.Skip)
                .Take(filter.Take)
                .ToListAsync(cancellationToken);

            return PagedResponse<T>.Create(
                items,
                filter.PageIndex,
                filter.PageSize,
                totalItems);
        }
    }

    extension<T>(List<T> items)
    {
        public PagedResponse<T> ToPagedResponse(PaginationQueryFilter filter, long totalItems)
        {
            return PagedResponse<T>.Create(
                [
                    .. items
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                ],
                filter.PageIndex,
                filter.PageSize,
                totalItems);
        }
    }
}
