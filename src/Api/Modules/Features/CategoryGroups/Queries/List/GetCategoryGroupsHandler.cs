namespace Api.Modules.Features.CategoryGroups.Queries.List;

public sealed class GetCategoryGroupsHandler(SogetecDbContext db) : IQueryHandler<GetCategoryGroupsQuery, List<GetCategoryGroupRecord>>
{
    public async ValueTask<List<GetCategoryGroupRecord>> Handle(GetCategoryGroupsQuery query, CancellationToken cancellationToken)
    {
        var entities = await db.CategoryGroups
                        .AsNoTracking()
                        .Include(c => c.Categories)
                        .Where(c => c.IsActive)
                        .Select(c => new GetCategoryGroupRecord(
                            Id: c.Id,
                            Name: c.Name,
                            ImageUrl: c.ImageUrl,
                            IsActive: c.IsActive,
                            SortOrder: c.SortOrder,
                            CreatedAt: c.CreatedOn,
                            LastModifiedAt: c.LastModifiedOn,
                            Categories: c.Categories.Select(c => new CategoryRecord(
                                Id: c.Id,
                                Name: c.Name,
                                Slug: c.Slug,
                                GroupId: c.GroupId,
                                GroupName: c.Group!.Name,
                                ParentId: c.ParentId,
                                ParentName: c.Parent == null ? null : c.Parent.Name,
                                IsActive: c.IsActive,
                                SortOrder: c.SortOrder,
                                CreatedAt: c.CreatedOn,
                                LastModifiedAt: c.LastModifiedOn
                            )).ToList()
                        ))
                        .ToListAsync(cancellationToken);

        return entities;
    }
}