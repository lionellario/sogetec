namespace Api.Modules.Features.ProductItems.Queries.GetById;

public record ProductSpecificationRecord(
    Guid Id,
    Guid ProductItemId,
    Guid ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    string HeaderName,
    string HeaderNameFr,
    string Value,
    bool IsVariant
);

public record GetProductItemByIdResponse(
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
    List<ProductSpecificationRecord> Specifications,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductItemByIdQuery(Guid Id) : IQuery<GetProductItemByIdResponse>;
