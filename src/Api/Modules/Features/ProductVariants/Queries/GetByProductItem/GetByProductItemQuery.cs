namespace Api.Modules.Features.ProductVariants.Queries.GetByProductItem;

public record GetByProductItemRecord(
    Guid Id,
    Guid ProductItemId,
    Guid ProductAttributeId,
    string ProductAttributeName,
    string ProductAttributeNameFr,
    string HeaderName,
    string HeaderNameFr,
    string Value,
    bool IsVariant
);

public record GetByProductItemQuery(Guid ProductItemId) : IQuery<List<GetByProductItemRecord>>;