using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Categories.Queries.List;

public record GetCategoryRecord(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    int GroupId,
    string GroupName,
    string GroupNameFr,
    int GroupSortOrder,
    string GroupImageUrl,
    int? ParentId,
    string? ParentName,
    int SortOrder,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoriesQuery(PaginationQueryFilter Filter) : IQuery<PagedResponse<GetCategoryRecord>>;
