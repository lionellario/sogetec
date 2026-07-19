using Sogetec.Chassis.Pagination;

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
    decimal Price,
    ProductQuantityUnit QuantityUnit,
    List<ProductImageDto> Images
);

public record GetProductByCategoryQuery(int CategoryId, PaginationQueryFilter Filter) : IQuery<PagedResponse<ProductByCategoryRecord>>;
