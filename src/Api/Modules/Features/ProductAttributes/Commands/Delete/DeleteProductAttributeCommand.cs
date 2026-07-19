namespace Api.Modules.Features.ProductAttributes.Commands.Delete;

public record DeleteProductAttributeCommand(Guid Id) : ICommand;