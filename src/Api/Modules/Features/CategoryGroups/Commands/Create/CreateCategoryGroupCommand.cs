namespace Api.Modules.Features.CategoryGroups.Commands.Create;

public record CreateCategoryGroupResponse(
    int Id,
    string Name,
    string? ImageUrl,
    int SortOrder,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateCategoryGroupCommand(
    int Id,
    string Name,
    string ImageUrl,
    int SortOrder
) : ICommand<CreateCategoryGroupResponse>;