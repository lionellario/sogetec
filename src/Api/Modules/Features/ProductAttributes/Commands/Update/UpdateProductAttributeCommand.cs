namespace Api.Modules.Features.ProductAttributes.Commands.Update;

public record UpdateProductAttributeResponse(
    Guid Id,
    string Name,
    string NameFr,
    Guid HeaderId,
    bool IsVariant,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateProductAttributeCommand(
    Guid Id,
    string Name,
    string NameFr,
    Guid HeaderId,
    bool IsVariant
) : ICommand<UpdateProductAttributeResponse>;