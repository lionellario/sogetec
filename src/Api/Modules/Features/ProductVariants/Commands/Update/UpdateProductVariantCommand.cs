namespace Api.Modules.Features.ProductVariants.Commands.Update;

public record UpdateProductVariantResponse(
    Guid Id,
    Guid ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    Guid ProductItemId,
    string Value,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateProductVariantCommand(
    Guid Id,
    string Value
) : ICommand<UpdateProductVariantResponse>;