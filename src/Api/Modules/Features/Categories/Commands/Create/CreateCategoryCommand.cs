namespace Api.Modules.Features.Categories.Commands.Create;

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

public record CreateCategoryCommand(
    Guid Id,
    string Name,
    Guid? ParentId,
    string? Description,
    string? ImageUrl
) : ICommand<UpdateCategoryResponse>;