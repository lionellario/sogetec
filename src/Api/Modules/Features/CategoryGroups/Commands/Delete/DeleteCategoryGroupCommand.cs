namespace Api.Modules.Features.CategoryGroups.Commands.Delete;

public record DeleteCategoryGroupCommand(List<Guid> Ids) : ICommand;