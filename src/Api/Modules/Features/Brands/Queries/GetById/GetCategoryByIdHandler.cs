namespace Api.Modules.Features.Brands.Queries.GetById;

public sealed class GetBrandByIdHandler(SogetecDbContext db) : IQueryHandler<GetBrandByIdQuery, GetBrandByIdResponse>
{
    public async ValueTask<GetBrandByIdResponse> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
    {
        var brand = await db.Brands.FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (brand is null)
        {
            throw NotFoundException.For<Brand>(
                query.Id,
                BrandErrorCode.NotFound);
        }

        return new(
            Id: brand.Id,
            Name: brand.Name,
            LogoUrl: brand.LogoUrl,
            CreatedAt: brand.CreatedOn,
            LastModifiedAt: brand.LastModifiedOn
        );
    }
}