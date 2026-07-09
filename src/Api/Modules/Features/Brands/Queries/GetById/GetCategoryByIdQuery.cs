namespace Api.Modules.Features.Brands.Queries.GetById;

public record GetBrandByIdResponse(
    int Id,
    string Name,
    string LogoUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetBrandByIdQuery(int Id) : IQuery<GetBrandByIdResponse>;