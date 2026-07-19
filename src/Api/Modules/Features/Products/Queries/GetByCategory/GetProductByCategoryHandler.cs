using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Products.Queries.GetByCategory;

public sealed class GetProductByCategoryHandler(SogetecDbContext db)
    : IQueryHandler<GetProductByCategoryQuery, PagedResponse<ProductByCategoryRecord>>
{
    public async ValueTask<PagedResponse<ProductByCategoryRecord>> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var count = await db.Products.CountAsync(cancellationToken);

        var products = await (
            from category in db.Categories.Where(x => x.Id == query.CategoryId && x.IsActive)
            from product in db.Products
                .Include(x => x.Images)
                .Include(x => x.Items)
                .Where(x => x.IsActive)
            select new ProductByCategoryRecord
            (
                Id: product.Id,
                Name: product.Name,
                NameFr: product.NameFr,
                Slug: product.Slug,
                Price: product.Price,
                QuantityUnit: product.QuantityUnit,
                Images: product.Images.Select(im => new ProductImageDto(
                    Id: im.Id,
                    ProductId: product.Id,
                    ImageUrl: im.Url,
                    ThumbnailImageUrl: im.PreviewUrl
                )).ToList()
            ))
            .Skip(query.Filter.Skip)
            .Take(query.Filter.Take)
            .ToListAsync(cancellationToken);

        var response = PagedResponse<ProductByCategoryRecord>.Create(products, query.Filter.PageIndex, query.Filter.PageSize, count);

        return response;
    }
}