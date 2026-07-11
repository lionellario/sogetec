namespace Api.Modules.Features.ProductItems.Queries.GetByProduct;

public record ProductSpecificationRecord(
    int Id,
    int ProductItemId,
    int ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    string HeaderName,
    string HeaderNameFr,
    string Value,
    bool IsVariant
);

public record GetProductItemByIdRecord(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    string Code,
    string Sku,
    string? Description,
    bool IsActive,
    int ProductId,
    decimal Price,
    decimal Cost,
    decimal InitialStock,
    decimal FinalStock,
    ProductQuantityUnit QuantityUnit,
    ProductItemDetailRecord? Details,
    List<ProductSpecificationRecord> Specifications,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductItemsByProductQuery(int ProdcutId, bool IncludeInactiveItems) : IQuery<List<GetProductItemByIdRecord>>;
