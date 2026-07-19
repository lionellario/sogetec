using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Categories.Queries.List;

public record GetCategoryRecord(
    Guid Id,
    string Name,
    string NameFr,
    string Slug,
    Guid GroupId,
    string GroupName,
    string GroupNameFr,
    int GroupSortOrder,
    string GroupImageUrl,
    Guid? ParentId,
    string? ParentName,
    int SortOrder,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoriesQuery(PaginationQueryFilter Filter) : IQuery<PagedResponse<GetCategoryRecord>>;
