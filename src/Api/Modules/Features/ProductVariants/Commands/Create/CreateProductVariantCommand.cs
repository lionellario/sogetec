namespace Api.Modules.Features.ProductVariants.Commands.Create;

public record CreateProductVariantResponse(
    int Id,
    int ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    int ProductItemId,
    string Value,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateProductVariantCommand(
    int Id,
    int ProductItemId,
    int ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    string Value
) : ICommand<CreateProductVariantResponse>;