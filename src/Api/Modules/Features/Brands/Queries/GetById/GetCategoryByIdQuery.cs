namespace Api.Modules.Features.Brands.Queries.GetById;

public record GetBrandByIdResponse(
    Guid Id,
    string Name,
    string LogoUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetBrandByIdQuery(Guid Id) : IQuery<GetBrandByIdResponse>;