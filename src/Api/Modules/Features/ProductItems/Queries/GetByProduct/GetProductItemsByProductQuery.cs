namespace Api.Modules.Features.ProductItems.Queries.GetByProduct;

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

public record GetProductItemByIdRecord(
    Guid Id,
    string Name,
    string NameFr,
    string Slug,
    string Code,
    string Sku,
    string? Description,
    bool IsActive,
    Guid ProductId,
    decimal Price,
    decimal Cost,
    decimal InitialStock,
    decimal FinalStock,
    ProductItemDetailRecord? Details,
    List<ProductSpecificationRecord> Specifications,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductItemsByProductQuery(Guid ProdcutId, bool IncludeInactiveItems) : IQuery<List<GetProductItemByIdRecord>>;
