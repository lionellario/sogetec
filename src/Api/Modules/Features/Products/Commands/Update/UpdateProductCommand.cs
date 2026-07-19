namespace Api.Modules.Features.Products.Commands.Update;

public record UpdateProductResponse(
    int Id
);

public record UpdateProductCommand(
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
) : ICommand<UpdateProductResponse>;