namespace Api.Modules.Features.Categories.Commands.Create;

public record CreateCategoryResponse(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    int GroupId,
    string GroupName,
    int? ParentId,
    string? ParentName,
    string? Description,
    bool IsActive,
    string? ImageUrl,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateCategoryCommand(
    int Id,
    string Name,
    string NameFr,
    int GroupId,
    int? ParentId,
    string? Description,
    string? ImageUrl
) : ICommand<CreateCategoryResponse>;