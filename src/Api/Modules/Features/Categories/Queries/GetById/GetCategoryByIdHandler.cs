namespace Api.Modules.Features.Categories.Queries.GetById;

public sealed class GetCategoryByIdHandler(SogetecDbContext db) : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
{
    public async ValueTask<GetCategoryByIdResponse> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (category is null)
        {
            throw NotFoundException.For<Category>(
                query.Id,
                CategoryErrorCode.CategoryNotFound);
        }

        return new(
            Id: category.Id,
            Name: category.Name,
            Slug: category.Slug,
            ParentId: category.Parent?.Id,
            ParentName: category.Parent?.Name,
            Description: category.Description,
            IsActive: category.IsActive,
            ImageUrl: category.ImageUrl,
            SortOrder: category.SortOrder
        );
    }
}