namespace Api.Modules.Features.ProductAttributeHeaders.Queries.List;

public record ItemRecord(
    Guid Id,
    string Name,
    string NameFr,
    Guid HeaderId,
    bool IsVariant
);

public record GetProductSpecificationModelRecord(
    Guid Id,
    string Name,
    string NameFr,
    int SortOrder,
    List<ItemRecord> Items,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductSpecificationModelsQuery : IQuery<List<GetProductSpecificationModelRecord>>;
