namespace Api.Modules.Features.Categories.Commands.Update;

public record UpdateCategoryResponse(
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

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string NameFr,
    Guid GroupId,
    Guid? ParentId,
    string? Description,
    string? ImageUrl,
    bool IsActive,
    int? SortOrder
) : ICommand<UpdateCategoryResponse>;