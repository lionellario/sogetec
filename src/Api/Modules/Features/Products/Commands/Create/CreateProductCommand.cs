namespace Api.Modules.Features.Products.Commands.Create;

public record CreateProductResponse(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    string Description,
    bool IsActive,
    string BrandUrl
);

public record CreateProductCommand(
    int Id,
    string Name,
    string NameFr,
    string Description,
    bool IsActive,
    int BrandId,
    int CategoryId
) : ICommand<CreateProductResponse>;
