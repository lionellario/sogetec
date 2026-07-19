namespace Api.Modules.Features.Products.Queries.GetById;

public record ProductItemDetailRecord(
    decimal ValeurInitialV0,
    decimal ValeurResiduelleVT,
    decimal ValeurResiduelleUnitaire,
    decimal AgeDays
);

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

public record ProductItemRecord(
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

public record ProductImageDto(
    Guid Id,
    Guid ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record GetProductByIdResponse(
    Guid Id,
    string Name,
    string NameFr,
    string Slug,
    bool IsActive,
    decimal Price,
    decimal Cost,
    decimal Stock,
    ProductQuantityUnit QuantityUnit,
    Guid CategoryId,
    string CategoryName,
    string CategoryNameFr,
    List<ProductItemRecord> Items,
    List<ProductImageDto> Images,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResponse>;
