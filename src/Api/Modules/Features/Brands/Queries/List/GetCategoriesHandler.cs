namespace Api.Modules.Features.Brands.Queries.List;

public sealed class GetBrandsHandler(SogetecDbContext db) : IQueryHandler<GetBrandsQuery, List<GetBrandRecord>>
{
    public async ValueTask<List<GetBrandRecord>> Handle(GetBrandsQuery query, CancellationToken cancellationToken)
    {
        var entities = await db.Brands
                        .AsNoTracking()
                        .Select(c => new GetBrandRecord(
                            Id: c.Id,
                            Name: c.Name,
                            LogoUrl: c.LogoUrl,
                            CreatedAt: c.CreatedOn,
                            LastModifiedAt: c.LastModifiedOn
                        ))
                        .ToListAsync(cancellationToken);

        return [.. entities.OrderBy(x => x.Name)];
    }
}