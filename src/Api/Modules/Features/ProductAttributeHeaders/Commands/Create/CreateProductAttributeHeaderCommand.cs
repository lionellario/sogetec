namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Create;

public record CreateProductAttributeHeaderResponse(
    int Id,
    string Name,
    string NameFr,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateProductAttributeHeaderCommand(
    int Id,
    string Name,
    string NameFr
) : ICommand<CreateProductAttributeHeaderResponse>;