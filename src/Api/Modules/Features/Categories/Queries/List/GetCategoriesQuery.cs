namespace Api.Modules.Features.Categories.Queries.List;

public record GetCategoryRecord(
    int Id,
    string Name,
    string Slug,
    int GroupId,
    string GroupName,
    int? ParentId,
    string? ParentName,
    int SortOrder,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoriesQuery : IQuery<List<GetCategoryRecord>>;
