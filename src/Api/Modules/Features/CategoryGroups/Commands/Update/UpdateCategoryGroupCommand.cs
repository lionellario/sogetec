namespace Api.Modules.Features.CategoryGroups.Commands.Update;

public record UpdateCategoryGroupResponse(
    int Id,
    string Name,
    string? ImageUrl,
    bool IsActive,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateCategoryGroupCommand(
    int Id,
    string Name,
    string ImageUrl,
    bool IsActive,
    int SortOrder
) : ICommand<UpdateCategoryGroupResponse>;