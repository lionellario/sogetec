namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Update;

public record UpdateProductAttributeHeaderResponse(
    int Id,
    string Name,
    string NameFr,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateProductAttributeHeaderCommand(
    int Id,
    string Name,
    string NameFr,
    int? SortOrder
) : ICommand<UpdateProductAttributeHeaderResponse>;