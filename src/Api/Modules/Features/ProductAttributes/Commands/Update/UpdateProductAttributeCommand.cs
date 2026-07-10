namespace Api.Modules.Features.ProductAttributes.Commands.Update;

public record UpdateProductAttributeResponse(
    int Id,
    string Name,
    string NameFr,
    int HeaderId,
    bool IsVariant,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateProductAttributeCommand(
    int Id,
    string Name,
    string NameFr,
    int HeaderId,
    bool IsVariant
) : ICommand<UpdateProductAttributeResponse>;