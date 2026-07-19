namespace Api.Modules.Features.ProductItems.Commands.Update;

public record UpdateProductItemResponse(
    Guid Id,
    string Name,
    string NameFr,
    string Slug,
    string Code,
    string Sku,
    string? Description,
    bool IsActive,
    Guid ProductId,
    decimal PriceAdjustment,
    decimal CostAdjustment,
    decimal InitialStock,
    decimal FinalStock,
    ProductItemDetailRecord? Details,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateProductItemCommand(
    Guid Id,
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