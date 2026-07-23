namespace Api.Modules.Features.CategoryGroups.Commands.Create;

public record CreateCategoryGroupResponse(
    Guid Id
);

public record CreateCategoryGroupCommand(
    Guid Id,
    string Name,
    string NameFr,
    string ImageUrl,
    int SortOrder,
    bool IsActive
) : ICommand<CreateCategoryGroupResponse>;