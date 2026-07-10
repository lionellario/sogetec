namespace Api.Modules.Features.ProductAttributes.Queries.GetById;

public sealed class GetProductAttributeByIdHandler(SogetecDbContext db)
    : IQueryHandler<GetProductAttributeByIdQuery, GetProductAttributeByIdResponse>
{
    public async ValueTask<GetProductAttributeByIdResponse> Handle(
        GetProductAttributeByIdQuery query,
        CancellationToken cancellationToken)
    {
        var attribute = await db.ProductAttributes.FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (attribute is null)
        {
            throw NotFoundException.For<ProductAttribute>(
                query.Id,
                ProductErrorCode.ProductAttributeNotFound);
        }

        return new(
            Id: attribute.Id,
            Name: attribute.Name,
            NameFr: attribute.NameFr,
            HeaderId: attribute.HeaderId,
            IsVariant: attribute.IsVariant,
            CreatedAt: attribute.CreatedOn,
            LastModifiedAt: attribute.LastModifiedOn
        );
    }
}