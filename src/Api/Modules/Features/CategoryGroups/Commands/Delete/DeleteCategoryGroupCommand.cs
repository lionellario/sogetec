namespace Api.Modules.Features.CategoryGroups.Commands.Delete;

public record DeleteCategoryGroupCommand(Guid Id) : ICommand;