namespace Api.Modules.Features.Products.Commands.Update;

public record UpdateProductResponse(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    string Description,
    bool IsActive
);

public record UpdateProductCommand(
    int Id,
    string Name,
    string NameFr,
    string Description,
    bool IsActive,
    int BrandId,
    int CategoryId
) : ICommand<UpdateProductResponse>;