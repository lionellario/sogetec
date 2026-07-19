namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Create;

public record CreateProductAttributeHeaderResponse(
    Guid Id,
    string Name,
    string NameFr,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateProductAttributeHeaderCommand(
    Guid Id,
    string Name,
    string NameFr
) : ICommand<CreateProductAttributeHeaderResponse>;