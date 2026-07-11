namespace Api.Modules.Features.ProductItems.Queries.GetById;

public sealed class GetProductItemByIdHandler(SogetecDbContext db) : IQueryHandler<GetProductItemByIdQuery, GetProductItemByIdResponse>
{
    public async ValueTask<GetProductItemByIdResponse> Handle(GetProductItemByIdQuery query, CancellationToken cancellationToken)
    {
        var variants = await db.ProductVariants
                                .Include(x => x.Attribute)
                                    .ThenInclude(a => a!.Header)
                                .Include(x => x.Item)
                                .Where(x => x.ItemId == query.Id)
                                .ToListAsync(cancellationToken);

        var productItem = variants.FirstOrDefault()?.Item;

        if (productItem is null)
        {
            throw NotFoundException.For<Category>(
                query.Id,
                CategoryErrorCode.CategoryNotFound);
        }

        return new(
            Id: productItem.Id,
            Name: productItem.Name,
            NameFr: productItem.NameFr,
            Slug: productItem.Slug,
            Code: productItem.Code,
            Sku: productItem.Sku,
            ProductId: productItem.ProductId,
            Price: productItem.Price,
            Cost: productItem.Cost,
            InitialStock: productItem.InitialStock,
            FinalStock: productItem.FinalStock,
            QuantityUnit: productItem.QuantityUnit,
            Description: productItem.Description,
            IsActive: productItem.IsActive,
            Details: productItem.Details is null ? null : new ProductItemDetailRecord(
                ValeurInitialV0: productItem.Details.ValeurInitialV0,
                ValeurResiduelleVT: productItem.Details.ValeurResiduelleVT,
                ValeurResiduelleUnitaire: productItem.Details.ValeurResiduelleUnitaire,
                AgeDays: productItem.Details.AgeDays
            ),
            Specifications: [.. variants.Select(c => new ProductSpecificationRecord(
                Id: c.Id,
                ProductItemId: c.ItemId,
                ProductAttributeId: c.AttributeId,
                ProductAttributeName: c.Attribute!.Name,
                ProductAttributeNameFr: c.Attribute!.NameFr,
                HeaderName: c.Attribute!.Header!.Name,
                HeaderNameFr: c.Attribute!.Header!.NameFr,
                Value: c.Value,
                IsVariant: c.Attribute.IsVariant
            ))],
            CreatedAt: productItem.CreatedOn,
            LastModifiedAt: productItem.LastModifiedOn
        );
    }
}