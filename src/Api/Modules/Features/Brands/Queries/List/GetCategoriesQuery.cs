namespace Api.Modules.Features.Brands.Queries.List;

public record GetBrandRecord(
    Guid Id,
    string Name,
    string LogoUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetBrandsQuery : IQuery<List<GetBrandRecord>>;
