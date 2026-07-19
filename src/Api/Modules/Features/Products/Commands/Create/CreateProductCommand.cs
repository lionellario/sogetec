namespace Api.Modules.Features.Products.Commands.Create;

public record CreateProductResponse(
    Guid Id
);

public record CreateProductCommand(
    Guid Id,
    string Name,
    string NameFr,
    string Description,
    bool IsActive,
    Guid BrandId,
    Guid CategoryId,
    decimal Price,
    decimal Cost,
    ProductQuantityUnit QuantityUnit
) : ICommand<CreateProductResponse>;
