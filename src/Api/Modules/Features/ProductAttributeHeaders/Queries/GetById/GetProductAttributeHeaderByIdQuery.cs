namespace Api.Modules.Features.ProductAttributeHeaders.Queries.GetById;

public record GetProductAttributeHeaderByIdResponse(
    int Id,
    string Name,
    string NameFr,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetProductAttributeHeaderByIdQuery(int Id) : IQuery<GetProductAttributeHeaderByIdResponse>;