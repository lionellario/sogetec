namespace Api.Modules.Features.Products.Queries.GetById;

public sealed class GetProductByIdHandler(SogetecDbContext db) : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async ValueTask<GetProductByIdResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await db.Products
                                .AsNoTracking()
                                // .Include(p => p.Items)
                                //     .ThenInclude(c => c.Variants)
                                //         .ThenInclude(v => v.Attribute)
                                //             .ThenInclude(a => a!.Header)
                                .Where(c => c.Id == query.Id)
                                .Select(product => new GetProductByIdResponse
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
                                    Items: product.Items.Select(productItem => new ProductItemRecord(
                                        Id: productItem.Id,
                                        Name: productItem.Name,
                                        NameFr: productItem.NameFr,
                                        Slug: productItem.Slug,
                                        Code: productItem.Code,
                                        Sku: productItem.Sku,
                                        Description: productItem.Description,
                                        IsActive: productItem.IsActive,
                                        ProductId: productItem.ProductId,
                                        PriceAdjustment: productItem.PriceAdjustment,
                                        CostAdjustment: productItem.CostAdjustment,
                                        InitialStock: productItem.InitialStock,
                                        FinalStock: productItem.FinalStock,
                                        Details: productItem.Details == null ? null : new ProductItemDetailRecord(
                                            ValeurInitialV0: productItem.Details.ValeurInitialV0,
                                            ValeurResiduelleVT: productItem.Details.ValeurResiduelleVT,
                                            ValeurResiduelleUnitaire: productItem.Details.ValeurResiduelleUnitaire,
                                            AgeDays: productItem.Details.AgeDays
                                        ),
                                        Specifications: productItem.Variants.Select(c => new ProductSpecificationRecord(
                                            Id: c.Id,
                                            ProductItemId: c.ItemId,
                                            ProductAttributeId: c.AttributeId,
                                            ProductAttributeName: c.Attribute!.Name,
                                            ProductAttributeNameFr: c.Attribute!.NameFr,
                                            HeaderName: c.Attribute!.Header!.Name,
                                            HeaderNameFr: c.Attribute!.Header!.NameFr,
                                            Value: c.Value,
                                            IsVariant: c.Attribute!.IsVariant
                                        )).ToList(),
                                        CreatedAt: productItem.CreatedOn,
                                        LastModifiedAt: productItem.LastModifiedOn
                                    )).ToList(),
                                    Images: product.Images.Select(im => new ProductImageDto(
                                        Id: im.Id,
                                        ProductId: product.Id,
                                        ImageUrl: im.Url,
                                        ThumbnailImageUrl: im.PreviewUrl
                                    )).ToList(),
                                    CreatedAt: product.CreatedOn,
                                    LastModifiedAt: product.LastModifiedOn
                                ))
                                .FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (product is null)
        {
            throw NotFoundException.For<Category>(
                query.Id,
                CategoryErrorCode.CategoryNotFound);
        }

        return product;
    }
}