namespace Api.Modules.Features.CategoryGroups.Commands.UpdateStatus;

public record UpdateCategoryGroupStatusCommand(List<Guid> Ids, bool IsActive) : ICommand;