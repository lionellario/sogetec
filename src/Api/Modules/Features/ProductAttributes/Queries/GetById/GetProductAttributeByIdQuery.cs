namespace Api.Modules.Features.ProductAttributes.Queries.GetById;

public record GetProductAttributeByIdResponse(
    int Id,
    string Name,
    string NameFr,
    int HeaderId,
    bool IsVariant,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductAttributeByIdQuery(int Id) : IQuery<GetProductAttributeByIdResponse>;