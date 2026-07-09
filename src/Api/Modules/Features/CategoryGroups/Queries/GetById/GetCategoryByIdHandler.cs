namespace Api.Modules.Features.CategoryGroups.Queries.GetById;

public sealed class GetCategoryByIdHandler(SogetecDbContext db) : IQueryHandler<GetCategoryGroupByIdQuery, GetCategoryGroupByIdResponse>
{
    public async ValueTask<GetCategoryGroupByIdResponse> Handle(GetCategoryGroupByIdQuery query, CancellationToken cancellationToken)
    {
        var categoryGroup = await db.CategoryGroups.FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (categoryGroup is null)
        {
            throw NotFoundException.For<Category>(
                query.Id,
                CategoryErrorCode.CategoryNotFound);
        }

        return new(
            Id: categoryGroup.Id,
            Name: categoryGroup.Name,
            IsActive: categoryGroup.IsActive,
            ImageUrl: categoryGroup.ImageUrl,
            SortOrder: categoryGroup.SortOrder,
            CreatedAt: categoryGroup.CreatedOn,
            LastModifiedAt: categoryGroup.LastModifiedOn
        );
    }
}