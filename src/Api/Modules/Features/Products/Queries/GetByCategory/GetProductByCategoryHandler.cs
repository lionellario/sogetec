using System.Linq.Expressions;

namespace Api.Modules.Features.Products.Queries.GetByCategory;

public sealed class GetProductByCategoryHandler(SogetecDbContext db) : IQueryHandler<GetProductByCategoryQuery, List<ProductByCategoryRecord>>
{
    public async ValueTask<List<ProductByCategoryRecord>> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Product, bool>> filter = x => x.CategoryId == query.CategoryId;
        if(!query.IncludeInactiveProducts)
        {
            filter = x => x.CategoryId == query.CategoryId && x.IsActive;
        }

        var items = await (
            from category in db.Categories.Where(x => x.Id == query.CategoryId && x.IsActive)
            from product in db.Products
                .Include(x => x.Images)
                .Include(x => x.Items)
                .Where(filter)
            select new
            {
                Id = product.Id,
                Name = product.Name,
                NameFr = product.NameFr,
                Slug = product.Slug,
                Description = product.Description,
                IsActive = product.IsActive,
                Prices = product.Items.Select(y => y.Price),
                Images = product.Images.Select(im => new ProductImageDto(
                    Id: im.Id,
                    ProductId: product.Id,
                    ImageUrl: im.Url,
                    ThumbnailImageUrl: im.PreviewUrl
                )).ToList()
            }).ToListAsync(cancellationToken);
    
        var products = items.Select(x => new ProductByCategoryRecord(
            Id: x.Id,
            Name: x.Name,
            NameFr: x.NameFr,
            Slug: x.Slug,
            Description: x.Description,
            IsActive: x.IsActive,
            Price: x.Prices.Min(),
            Images: x.Images)).ToList();
        
        return products;
    }
}