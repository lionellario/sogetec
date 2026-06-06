namespace Api.Modules.Features.Categories.Commands.Update;

public record UpdateCategoryResponse(
    Guid Id,
    string Name,
    string Slug,
    Guid? ParentId,
    string? ParentName,
    string? Description,
    bool IsActive,
    string? ImageUrl,
    int SortOrder
);

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    Guid? ParentId,
    string? Description,
    string? ImageUrl,
    int? SortOrder
) : ICommand<UpdateCategoryResponse>;