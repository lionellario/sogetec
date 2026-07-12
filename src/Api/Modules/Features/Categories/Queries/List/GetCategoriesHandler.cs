namespace Api.Modules.Features.Categories.Queries.List;

public sealed class GetCategoriesHandler(SogetecDbContext db) : IQueryHandler<GetCategoriesQuery, List<GetCategoryRecord>>
{
    public async ValueTask<List<GetCategoryRecord>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var entities = await db.Categories
                        .AsNoTracking()
                        .Include(c => c.Group)
                        .Select(c => new GetCategoryRecord(
                            Id: c.Id,
                            Name: c.Name,
                            NameFr: c.NameFr,
                            Slug: c.Slug,
                            GroupId: c.GroupId,
                            GroupName: c.Group!.Name,
                            GroupNameFr: c.Group!.NameFr,
                            GroupSortOrder: c.Group!.SortOrder,
                            GroupImageUrl: c.Group!.ImageUrl,
                            ParentId: c.ParentId,
                            ParentName: c.Parent == null ? null : c.Parent.Name,
                            SortOrder: c.SortOrder,
                            IsActive: c.IsActive,
                            CreatedAt: c.CreatedOn,
                            LastModifiedAt: c.LastModifiedOn
                        ))
                        .ToListAsync(cancellationToken);

        return entities;
    }
}