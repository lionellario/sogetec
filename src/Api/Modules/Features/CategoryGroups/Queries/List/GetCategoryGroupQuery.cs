namespace Api.Modules.Features.CategoryGroups.Queries.List;

public record CategoryRecord(
    Guid Id,
    string Name,
    string NameFr,
    string Slug,
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
    string NameFr,
    string ImageUrl,
    bool IsActive,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt,
    List<CategoryRecord> Categories
);

public record GetCategoryGroupsQuery(bool? IncludeInactive = null) : IQuery<List<GetCategoryGroupRecord>>;
