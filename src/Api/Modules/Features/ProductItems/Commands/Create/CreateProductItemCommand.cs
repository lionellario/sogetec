namespace Api.Modules.Features.ProductItems.Commands.Create;

public record CreateProductItemResponse(
    Guid Id,
    string Name,
    string NameFr,
    string Slug,
    string Code,
    string Sku,
    string? Description,
    bool IsActive,
    Guid ProductId,
    decimal PriceAdjustment,
    decimal CostAdjustment,
    decimal InitialStock,
    decimal FinalStock,
    ProductItemDetailRecord? Details,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateProductVariantRecord(
    Guid ProductAttributeId,
    string Value
);

public record CreateProductItemCommand(
    Guid Id,
    string Name,
    string NameFr,
    string Code,
    string Sku,
    string? Description,
    bool IsActive,
    Guid ProductId,
    decimal PriceAdjustment,
    decimal CostAdjustment,
    decimal InitialStock,
    decimal FinalStock,
    ProductItemDetailRecord? Details,
    List<CreateProductVariantRecord> Specifications
) : ICommand<CreateProductItemResponse>;