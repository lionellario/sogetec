namespace Api.Modules.Features.ProductAttributes.Queries.List;

public record GetProductAttributeRecord(
    int Id,
    string Name,
    string NameFr,
    int HeaderId,
    bool IsVariant,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductAttributesQuery : IQuery<List<GetProductAttributeRecord>>;
