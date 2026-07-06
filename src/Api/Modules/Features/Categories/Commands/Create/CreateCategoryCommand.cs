namespace Api.Modules.Features.Categories.Commands.Create;

public record CreateCategoryResponse(
    int Id,
    string Name,
    string Slug,
    int? ParentId,
    string? ParentName,
    string? Description,
    bool IsActive,
    string? ImageUrl,
    int SortOrder
);

public record CreateCategoryCommand(
    int Id,
    string Name,
    int? ParentId,
    string? Description,
    string? ImageUrl
) : ICommand<CreateCategoryResponse>;