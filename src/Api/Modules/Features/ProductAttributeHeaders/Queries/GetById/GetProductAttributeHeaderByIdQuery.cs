namespace Api.Modules.Features.ProductAttributeHeaders.Queries.GetById;

public record GetProductAttributeHeaderByIdResponse(
    Guid Id,
    string Name,
    string NameFr,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductAttributeHeaderByIdQuery(Guid Id) : IQuery<GetProductAttributeHeaderByIdResponse>;