namespace Api.Modules.Features.ProductVariants.Queries.GetByProductItem;

public record GetByProductItemRecord(
    int Id,
    int ProductItemId,
    int ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    string HeaderName,
    string HeaderNameFr,
    string Value,
    bool IsVariant
);

public record GetByProductItemQuery(int ProductItemId) : IQuery<List<GetByProductItemRecord>>;