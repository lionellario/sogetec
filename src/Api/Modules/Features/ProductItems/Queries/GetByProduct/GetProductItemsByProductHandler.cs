namespace Api.Modules.Features.ProductItems.Queries.GetByProduct;

public sealed class GetCategoriesHandler(SogetecDbContext db) : IQueryHandler<GetProductItemsByProductQuery, List<GetProductItemByIdRecord>>
{
    public async ValueTask<List<GetProductItemByIdRecord>> Handle(GetProductItemsByProductQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = db.ProductItems
                            .AsNoTracking()
                            .Include(c => c.Variants)
                                .ThenInclude(v => v.Attribute)
                                    .ThenInclude(a => a!.Header)
                            .Where(c => c.ProductId == query.ProdcutId)
                            .Select(productItem => new GetProductItemByIdRecord(
                                Id: productItem.Id,
                                Name: productItem.Name,
                                NameFr: productItem.NameFr,
                                Slug: productItem.Slug,
                                Code: productItem.Code,
                                Sku: productItem.Sku,
                                Description: productItem.Description,
                                IsActive: productItem.IsActive,
                                ProductId: productItem.ProductId,
                                Price: productItem.Price,
                                Cost: productItem.Cost,
                                InitialStock: productItem.InitialStock,
                                FinalStock: productItem.FinalStock,
                                QuantityUnit: productItem.QuantityUnit,
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
                            ));

        if (!query.IncludeInactiveItems)
        {
            dbQuery = dbQuery.Where(x => x.IsActive);
        }

        var productItems = await dbQuery.ToListAsync(cancellationToken);

        return productItems;
    }
}