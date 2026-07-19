using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Products.Queries.GetProducts;

public record ProductImageDto(
    int Id,
    int ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record ProductRecord(
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
    List<ProductImageDto> Images,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductsQuery(PaginationQueryFilter Filter) : IQuery<PagedResponse<ProductRecord>>;
