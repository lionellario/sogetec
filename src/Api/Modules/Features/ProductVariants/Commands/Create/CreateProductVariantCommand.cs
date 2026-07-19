namespace Api.Modules.Features.ProductVariants.Commands.Create;

public record CreateProductVariantResponse(
    Guid Id,
    Guid ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    Guid ProductItemId,
    string Value,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateProductVariantCommand(
    Guid Id,
    Guid ProductItemId,
    Guid ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    string Value
) : ICommand<CreateProductVariantResponse>;