namespace Api.Modules.Features.ProductVariants.Commands.Delete;

public record DeleteProductVariantCommand(Guid Id) : ICommand;