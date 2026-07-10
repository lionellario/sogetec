namespace Api.Modules.Features.ProductAttributes.Commands.Create;

public record CreateProductAttributeResponse(
    int Id,
    string Name,
    string NameFr,
    int HeaderId,
    bool IsVariant,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateProductAttributeCommand(
    int Id,
    string Name,
    string NameFr,
    int HeaderId,
    bool IsVariant
) : ICommand<CreateProductAttributeResponse>;