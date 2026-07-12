namespace Api.Modules.Features.Products.Queries.GetById;

public sealed class GetProductByIdHandler(SogetecDbContext db) : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async ValueTask<GetProductByIdResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await db.Products.FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (product is null)
        {
            throw NotFoundException.For<Category>(
                query.Id,
                CategoryErrorCode.CategoryNotFound);
        }

        return new(
            Id: product.Id,
            Name: product.Name,
            NameFr: product.NameFr,
            Slug: product.Slug,
            Description: product.Description,
            IsActive: product.IsActive
        );
    }
}