namespace Api.Modules.Features.ProductAttributes.Queries.GetById;

public record GetProductAttributeByIdResponse(
    Guid Id,
    string Name,
    string NameFr,
    Guid HeaderId,
    bool IsVariant,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductAttributeByIdQuery(Guid Id) : IQuery<GetProductAttributeByIdResponse>;