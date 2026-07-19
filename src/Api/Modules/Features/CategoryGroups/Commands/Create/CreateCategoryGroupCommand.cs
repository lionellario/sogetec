namespace Api.Modules.Features.CategoryGroups.Commands.Create;

public record CreateCategoryGroupResponse(
    Guid Id,
    string Name,
    string NameFr,
    string? ImageUrl,
    int SortOrder,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateCategoryGroupCommand(
    Guid Id,
    string Name,
    string NameFr,
    string ImageUrl,
    int SortOrder
) : ICommand<CreateCategoryGroupResponse>;