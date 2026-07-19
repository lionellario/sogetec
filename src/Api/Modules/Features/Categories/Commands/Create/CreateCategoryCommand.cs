namespace Api.Modules.Features.Categories.Commands.Create;

public record CreateCategoryResponse(
    Guid Id,
    string Name,
    string NameFr,
    string Slug,
    Guid GroupId,
    string GroupName,
    Guid? ParentId,
    string? ParentName,
    string? Description,
    bool IsActive,
    string? ImageUrl,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateCategoryCommand(
    Guid Id,
    string Name,
    string NameFr,
    Guid GroupId,
    Guid? ParentId,
    string? Description,
    string? ImageUrl,
    bool IsActive
) : ICommand<CreateCategoryResponse>;