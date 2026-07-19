using System.Linq.Expressions;
using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Products.Queries.GetProducts;

public sealed class GetProductsHandler(SogetecDbContext db) : IQueryHandler<GetProductsQuery, PagedResponse<ProductRecord>>
{
    public async ValueTask<PagedResponse<ProductRecord>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var count = await db.Products.CountAsync(cancellationToken);

        var products = await db.Products
                        .Select(product => new ProductRecord
                        (
                            Id: product.Id,
                            Name: product.Name,
                            NameFr: product.NameFr,
                            Slug: product.Slug,
                            IsActive: product.IsActive,
                            Price: product.Price,
                            Cost: product.Cost,
                            Stock: product.Items.Sum(x => x.FinalStock),
                            QuantityUnit: product.QuantityUnit,
                            CategoryId: product.CategoryId,
                            CategoryName: product.Category!.Name,
                            CategoryNameFr: product.Category.NameFr,
                            Images: product.Images.Select(im => new ProductImageDto(
                                Id: im.Id,
                                ProductId: product.Id,
                                ImageUrl: im.Url,
                                ThumbnailImageUrl: im.PreviewUrl
                            )).ToList(),
                            CreatedAt: product.CreatedOn,
                            LastModifiedAt: product.LastModifiedOn
                        ))
                        .Skip(query.Filter.Skip)
                        .Take(query.Filter.Take)
                        .ToListAsync(cancellationToken);

        var response = PagedResponse<ProductRecord>.Create(products, query.Filter.PageIndex, query.Filter.PageSize, count);

        return response;
    }
}