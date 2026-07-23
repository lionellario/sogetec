namespace Api.Modules.Features.CategoryGroups.Commands.Update;

public record UpdateCategoryGroupResponse(
    Guid Id
);

public record UpdateCategoryGroupCommand(
    Guid Id,
    string Name,
    string NameFr,
    string ImageUrl,
    bool IsActive,
    int SortOrder
) : ICommand<UpdateCategoryGroupResponse>;