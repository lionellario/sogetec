namespace Api.Modules.Features.Products.Commands.Create;

public record CreateProductResponse(
    int Id
);

public record CreateProductCommand(
    int Id,
    string Name,
    string NameFr,
    string Description,
    bool IsActive,
    int BrandId,
    int CategoryId,
    decimal Price,
    decimal Cost,
    ProductQuantityUnit QuantityUnit
) : ICommand<CreateProductResponse>;
