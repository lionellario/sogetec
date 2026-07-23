namespace Api.Modules.Features.Products.Commands.UpdateStatus;

public record UpdateProductStatusCommand(List<Guid> Ids, bool IsActive) : ICommand;