using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Products.Queries.GetByCategory;

public record ProductImageDto(
    Guid Id,
    Guid ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record ProductByCategoryRecord(
    Guid Id,
    string Name,
    string NameFr,
    string Slug,
    decimal Price,
    ProductQuantityUnit QuantityUnit,
    List<ProductImageDto> Images
);

public record GetProductByCategoryQuery(Guid CategoryId, PaginationQueryFilter Filter) : IQuery<PagedResponse<ProductByCategoryRecord>>;
