namespace Api.Modules.Features.CategoryGroups.Queries.List;

public sealed class GetCategoryGroupsHandler(SogetecDbContext db) : IQueryHandler<GetCategoryGroupsQuery, List<GetCategoryGroupRecord>>
{
    public async ValueTask<List<GetCategoryGroupRecord>> Handle(GetCategoryGroupsQuery query, CancellationToken cancellationToken)
    {
        var baseQuery = db.CategoryGroups.AsNoTracking();

        if (query.IncludeInactive == false)
        {
            baseQuery = baseQuery.Where(c => c.IsActive);
        }

        var dbQuery = baseQuery.Select(c => new GetCategoryGroupRecord(
            Id: c.Id,
            Name: c.Name,
            NameFr: c.NameFr,
            ImageUrl: c.ImageUrl,
            IsActive: c.IsActive,
            SortOrder: c.SortOrder,
            CreatedAt: c.CreatedOn,
            LastModifiedAt: c.LastModifiedOn,
            Categories: c.Categories.Select(c => new CategoryRecord(
                Id: c.Id,
                Name: c.Name,
                NameFr: c.NameFr,
                Slug: c.Slug,
                ParentId: c.ParentId,
                ParentName: c.Parent == null ? null : c.Parent.Name,
                IsActive: c.IsActive,
                SortOrder: c.SortOrder,
                CreatedAt: c.CreatedOn,
                LastModifiedAt: c.LastModifiedOn
            )).ToList()
        ));

        var entities = await dbQuery.ToListAsync(cancellationToken);

        return [.. entities.OrderBy(x => x.SortOrder)];
    }
}