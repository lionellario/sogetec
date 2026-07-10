namespace Api.Modules.Features.ProductAttributeHeaders.Queries.List;

public record ItemRecord(
    int Id,
    string Name,
    string NameFr,
    int HeaderId,
    bool IsVariant
);

public record GetProductSpecificationModelRecord(
    int Id,
    string Name,
    string NameFr,
    int SortOrder,
    List<ItemRecord> Items,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductSpecificationModelsQuery : IQuery<List<GetProductSpecificationModelRecord>>;
