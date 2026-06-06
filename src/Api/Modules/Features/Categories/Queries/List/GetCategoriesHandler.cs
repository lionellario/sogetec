namespace Api.Modules.Features.Categories.Queries.List;

public sealed class GetCategoriesHandler(SogetecDbContext db) : IQueryHandler<GetCategoriesQuery, List<GetCategoryDto>>
{
    public async ValueTask<List<GetCategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var entities = await db.Categories
                        .AsNoTracking()
                        .Where(c => c.IsActive)
                        .Select(c => new GetCategoryDto(
                            Id: c.Id,
                            Name: c.Name,
                            Slug: c.Slug,
                            ParentId: c.ParentId,
                            ParentName: c.Parent == null ? null : c.Parent.Name
                        ))
                        .ToListAsync(cancellationToken);

        return entities;
    }
}