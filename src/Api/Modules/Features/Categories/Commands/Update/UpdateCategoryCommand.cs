namespace Api.Modules.Features.Categories.Commands.Update;

public record UpdateCategoryResponse(
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

public record UpdateCategoryCommand(
    int Id,
    string Name,
    int? ParentId,
    string? Description,
    string? ImageUrl,
    int? SortOrder
) : ICommand<UpdateCategoryResponse>;