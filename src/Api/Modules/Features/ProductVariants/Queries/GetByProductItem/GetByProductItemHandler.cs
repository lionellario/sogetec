namespace Api.Modules.Features.ProductVariants.Queries.GetByProductItem;

public sealed class GetByProductItemHandler(SogetecDbContext db)
    : IQueryHandler<GetByProductItemQuery, List<GetByProductItemRecord>>
{
    public async ValueTask<List<GetByProductItemRecord>> Handle(
        GetByProductItemQuery command,
        CancellationToken cancellationToken)
    {
        var variants = await db.ProductVariants
                                .AsNoTracking()
                                .Where(c => c.ItemId == command.ProductItemId)
                                .Select(c => new GetByProductItemRecord(
                                    Id: c.Id,
                                    ProductItemId: c.ItemId,
                                    ProductAttributeId: c.AttributeId,
                                    ProductAttributeName: c.Attribute!.Name,
                                    ProductAttributeNameFr: c.Attribute!.NameFr,
                                    HeaderName: c.Attribute!.Header!.Name,
                                    HeaderNameFr: c.Attribute!.Header!.NameFr,
                                    Value: c.Value,
                                    IsVariant: c.Attribute.IsVariant
                                )).ToListAsync(cancellationToken);

        return variants;
    }
}
