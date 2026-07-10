namespace Api.Modules.Features.ProductVariants.Commands.Update;

public record UpdateProductVariantResponse(
    int Id,
    int ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    int ProductItemId,
    string Value,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateProductVariantCommand(
    int Id,
    string Value
) : ICommand<UpdateProductVariantResponse>;