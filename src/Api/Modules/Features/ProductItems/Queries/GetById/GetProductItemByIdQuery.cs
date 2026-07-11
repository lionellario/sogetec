namespace Api.Modules.Features.ProductItems.Queries.GetById;

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

public record GetProductItemByIdResponse(
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

public record GetProductItemByIdQuery(int Id) : IQuery<GetProductItemByIdResponse>;
