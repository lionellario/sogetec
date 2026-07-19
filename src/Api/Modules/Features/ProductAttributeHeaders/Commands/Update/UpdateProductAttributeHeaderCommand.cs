namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Update;

public record UpdateProductAttributeHeaderResponse(
    Guid Id,
    string Name,
    string NameFr,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateProductAttributeHeaderCommand(
    Guid Id,
    string Name,
    string NameFr,
    int? SortOrder
) : ICommand<UpdateProductAttributeHeaderResponse>;