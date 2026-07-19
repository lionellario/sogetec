namespace Api.Modules.Features.Products.Commands.Update;

public record UpdateProductResponse(
    Guid Id
);

public record UpdateProductCommand(
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
) : ICommand<UpdateProductResponse>;