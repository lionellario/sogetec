using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Products.Queries.GetProducts;

public record ProductImageDto(
    Guid Id,
    Guid ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record ProductRecord(
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
    List<ProductImageDto> Images,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductsQuery(PaginationQueryFilter Filter) : IQuery<PagedResponse<ProductRecord>>;
