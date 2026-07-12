using Sogetec.Chassis.Domain;
using Sogetec.Chassis.Extensions;

namespace Api.Modules.Entities;

public sealed class ProductItem : Entity
{
    public string Name { get; internal set; } = default!;
    public string NameFr { get; internal set; } = default!;
    public string Slug { get; internal set; } = default!;
    public string Code { get; internal set; } = default!;
    public string Sku { get; internal set; } = default!;
    public string? Description { get; internal set; }
    public bool IsActive { get; internal set; }
    public int ProductId { get; internal set; }
    public Product? Product { get; internal set; }
    public decimal Price { get; internal set; }
    public decimal Cost { get; internal set; }
    public decimal InitialStock { get; internal set; }
    public decimal FinalStock { get; internal set; }
    public ProductQuantityUnit QuantityUnit { get; internal set; } = ProductQuantityUnit.Piece;
    public ICollection<ProductVariant> Variants { get; internal set; } = [];
    public ProductItemDetails? Details { get; internal set; }

    public static ProductItem Create(
        string name,
        string nameFr,
        string code,
        string sku,
        Product product,
        decimal price,
        decimal cost,
        decimal initialStock,
        decimal finalStock,
        ProductQuantityUnit quantityUnit = ProductQuantityUnit.Piece,
        string? description = null,
        ProductItemDetails? Details = null,
        bool isActive = false)
        => new()
        {
            Name = name,
            NameFr = nameFr,
            Code = code,
            Slug = name.Slugify(),
            Sku = sku,
            ProductId = product.Id,
            Product = product,
            Price = price,
            Cost = cost,
            InitialStock = initialStock,
            FinalStock = finalStock,
            QuantityUnit = quantityUnit,
            Description = description,
            Details = Details,
            IsActive = isActive
        };

    public override bool Equals(object? obj) => obj is ProductItem at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}

public class ProductItemDetails
{
    public decimal ValeurInitialV0 { get; set; }
    public decimal ValeurResiduelleVT { get; set; }
    public decimal ValeurResiduelleUnitaire { get; set; }
    public decimal AgeDays { get; set; }
}