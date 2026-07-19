namespace Api.Modules.Features.ProductAttributes.Queries.List;

public record GetProductAttributeRecord(
    Guid Id,
    string Name,
    string NameFr,
    Guid HeaderId,
    bool IsVariant,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductAttributesQuery : IQuery<List<GetProductAttributeRecord>>;
