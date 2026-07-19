namespace Api.Modules.Features.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : ICommand;