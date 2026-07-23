namespace Api.Modules.Features.ProductItems.Queries.GetByProduct;

public sealed class GetCategoriesHandler(SogetecDbContext db) : IQueryHandler<GetProductItemsByProductQuery, List<GetProductItemByIdRecord>>
{
    public async ValueTask<List<GetProductItemByIdRecord>> Handle(GetProductItemsByProductQuery query, CancellationToken cancellationToken)
    {
        var baseQuery = db.ProductItems.AsNoTracking();

        if (query.IncludeInactive == false)
        {
            baseQuery = baseQuery.Where(c => c.IsActive);
        }

        var dbQuery = baseQuery
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
                            Price: productItem.PriceAdjustment,
                            Cost: productItem.CostAdjustment,
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
                        ));

        var productItems = await dbQuery.ToListAsync(cancellationToken);

        return productItems;
    }
}