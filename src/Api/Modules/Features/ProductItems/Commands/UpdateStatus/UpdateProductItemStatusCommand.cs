namespace Api.Modules.Features.ProductItems.Commands.UpdateStatus;

public record UpdateProductItemStatusCommand(List<Guid> Ids, bool IsActive) : ICommand;