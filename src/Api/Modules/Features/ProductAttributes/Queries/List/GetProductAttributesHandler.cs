namespace Api.Modules.Features.ProductAttributes.Queries.List;

public sealed class GetProductAttributesHandler(SogetecDbContext db)
    : IQueryHandler<GetProductAttributesQuery, List<GetProductAttributeRecord>>
{
    public async ValueTask<List<GetProductAttributeRecord>> Handle(
        GetProductAttributesQuery query,
        CancellationToken cancellationToken)
    {
        var entities = await db.ProductAttributes
                        .AsNoTracking()
                        .Select(c => new GetProductAttributeRecord(
                            Id: c.Id,
                            Name: c.Name,
                            NameFr: c.NameFr,
                            HeaderId: c.HeaderId,
                            IsVariant: c.IsVariant,
                            CreatedAt: c.CreatedOn,
                            LastModifiedAt: c.LastModifiedOn
                        ))
                        .ToListAsync(cancellationToken);

        return entities;
    }
}