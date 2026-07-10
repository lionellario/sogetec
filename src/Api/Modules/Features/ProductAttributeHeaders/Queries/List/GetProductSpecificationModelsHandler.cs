namespace Api.Modules.Features.ProductAttributeHeaders.Queries.List;

public sealed class GetProductSpecificationModelsHandler(SogetecDbContext db)
    : IQueryHandler<GetProductSpecificationModelsQuery, List<GetProductSpecificationModelRecord>>
{
    public async ValueTask<List<GetProductSpecificationModelRecord>> Handle(
        GetProductSpecificationModelsQuery query,
        CancellationToken cancellationToken)
    {
        var entities = await db.ProductAttributeHeaders
                        .AsNoTracking()
                        .Include(c => c.Attributes)
                        .Select(c => new GetProductSpecificationModelRecord(
                            Id: c.Id,
                            Name: c.Name,
                            NameFr: c.NameFr,
                            SortOrder: c.SortOrder,
                            Items: c.Attributes.Select(c => new ItemRecord(
                                Id: c.Id,
                                Name: c.Name,
                                NameFr: c.NameFr,
                                HeaderId: c.HeaderId,
                                IsVariant: c.IsVariant
                            )).ToList(),
                            CreatedAt: c.CreatedOn,
                            LastModifiedAt: c.LastModifiedOn
                        ))
                        .ToListAsync(cancellationToken);

        return entities;
    }
}