namespace Api.Modules.Features.ProductItems.Commands.Update;

public record UpdateProductItemResponse(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    string Code,
    string Sku,
    string? Description,
    bool IsActive,
    int ProductId,
    decimal PriceAdjustment,
    decimal CostAdjustment,
    decimal InitialStock,
    decimal FinalStock,
    ProductItemDetailRecord? Details,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateProductItemCommand(
    int Id,
    string Name,
    string NameFr,
    string Code,
    string Sku,
    string? Description,
    bool IsActive,
    decimal PriceAdjustment,
    decimal CostAdjustment,
    decimal InitialStock,
    decimal FinalStock,
    ProductItemDetailRecord? Details
) : ICommand<UpdateProductItemResponse>;