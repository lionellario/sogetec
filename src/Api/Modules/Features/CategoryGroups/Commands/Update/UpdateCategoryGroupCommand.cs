namespace Api.Modules.Features.CategoryGroups.Commands.Update;

public record UpdateCategoryGroupResponse(
    Guid Id,
    string Name,
    string? ImageUrl,
    bool IsActive,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateCategoryGroupCommand(
    Guid Id,
    string Name,
    string ImageUrl,
    bool IsActive,
    int SortOrder
) : ICommand<UpdateCategoryGroupResponse>;