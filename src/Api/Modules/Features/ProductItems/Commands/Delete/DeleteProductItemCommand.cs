namespace Api.Modules.Features.ProductItems.Commands.Delete;

public record DeleteProductItemCommand(Guid Id) : ICommand;