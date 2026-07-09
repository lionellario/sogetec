namespace Api.Modules.Features.CategoryGroups.Queries.List;

public record CategoryRecord(
    int Id,
    string Name,
    string Slug,
    int GroupId,
    string GroupName,
    int? ParentId,
    string? ParentName,
    bool IsActive,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoryGroupRecord(
    int Id,
    string Name,
    string ImageUrl,
    bool IsActive,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt,
    List<CategoryRecord> Categories
);

public record GetCategoryGroupsQuery : IQuery<List<GetCategoryGroupRecord>>;
