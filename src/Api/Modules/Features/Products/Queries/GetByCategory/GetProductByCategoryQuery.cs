namespace Api.Modules.Features.Products.Queries.GetByCategory;

public record ProductImageDto(
    int Id,
    int ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record ProductByCategoryRecord(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    string Description,
    bool IsActive,
    decimal Price,
    List<ProductImageDto> Images
);

public record GetProductByCategoryQuery(int CategoryId, bool IncludeInactiveProducts) : IQuery<List<ProductByCategoryRecord>>;
