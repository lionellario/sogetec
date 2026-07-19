namespace Api.Modules.Features.ProductAttributes.Commands.Create;

public record CreateProductAttributeResponse(
    Guid Id,
    string Name,
    string NameFr,
    Guid HeaderId,
    bool IsVariant,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateProductAttributeCommand(
    Guid Id,
    string Name,
    string NameFr,
    Guid HeaderId,
    bool IsVariant
) : ICommand<CreateProductAttributeResponse>;