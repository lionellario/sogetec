namespace Api.Modules.Features.CategoryGroups.Queries.List;

public record CategoryRecord(
    Guid Id,
    string Name,
    string Slug,
    Guid GroupId,
    string GroupName,
    Guid? ParentId,
    string? ParentName,
    bool IsActive,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoryGroupRecord(
    Guid Id,
    string Name,
    string ImageUrl,
    bool IsActive,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt,
    List<CategoryRecord> Categories
);

public record GetCategoryGroupsQuery(bool IncludeInactive) : IQuery<List<GetCategoryGroupRecord>>;
