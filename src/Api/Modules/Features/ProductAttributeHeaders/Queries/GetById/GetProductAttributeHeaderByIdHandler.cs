namespace Api.Modules.Features.ProductAttributeHeaders.Queries.GetById;

public sealed class GetProductAttributeHeaderByIdHandler(SogetecDbContext db)
    : IQueryHandler<GetProductAttributeHeaderByIdQuery, GetProductAttributeHeaderByIdResponse>
{
    public async ValueTask<GetProductAttributeHeaderByIdResponse> Handle(
        GetProductAttributeHeaderByIdQuery query,
        CancellationToken cancellationToken)
    {
        var header = await db.ProductAttributeHeaders.FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (header is null)
        {
            throw NotFoundException.For<ProductAttributeHeader>(
                query.Id,
                ProductErrorCode.ProductAttributeHeaderNotFound);
        }

        return new(
            Id: header.Id,
            Name: header.Name,
            NameFr: header.NameFr,
            SortOrder: header.SortOrder,
            CreatedAt: header.CreatedOn,
            LastModifiedAt: header.LastModifiedOn
        );
    }
}