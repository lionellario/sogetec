namespace Api.Modules.Features.Products.Queries.GetById;

public record ProductItemDetailRecord(
    decimal ValeurInitialV0,
    decimal ValeurResiduelleVT,
    decimal ValeurResiduelleUnitaire,
    decimal AgeDays
);

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

public record ProductItemRecord(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    string Code,
    string Sku,
    string? Description,
    bool IsActive,
    int ProductId,
    decimal PriceAdjustment,
    decimal CostAdjustment,
    decimal InitialStock,
    decimal FinalStock,
    ProductItemDetailRecord? Details,
    List<ProductSpecificationRecord> Specifications,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record ProductImageDto(
    int Id,
    int ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record GetProductByIdResponse(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    bool IsActive,
    decimal Price,
    decimal Cost,
    decimal Stock,
    ProductQuantityUnit QuantityUnit,
    int CategoryId,
    string CategoryName,
    string CategoryNameFr,
    List<ProductItemRecord> Items,
    List<ProductImageDto> Images,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductByIdQuery(int Id) : IQuery<GetProductByIdResponse>;
