using Sogetec.Chassis.Domain;
using Sogetec.Chassis.Extensions;

namespace Api.Modules.Entities;

public sealed class Product : Entity
{
    public string Name { get; internal set; } = default!;
    public string NameFr { get; internal set; } = default!;
    public string Slug { get; internal set; } = default!;
    public string Description { get; internal set; } = default!;
    public bool IsActive { get; internal set; }
    public int BrandId { get; internal set; }
    public Brand? Brand { get; internal set; }
    public int CategoryId { get; internal set; }
    public Category? Category { get; internal set; }
    public decimal Price { get; internal set; }
    public decimal Cost { get; internal set; }
    public ProductQuantityUnit QuantityUnit { get; internal set; } = ProductQuantityUnit.Piece;
    public ICollection<ProductItem> Items { get; internal set; } = [];
    public ICollection<ProductImage> Images { get; internal set; } = [];

    public static Product Create(
        string name,
        string nameFr,
        string description,
        Brand brand,
        Category category,
        decimal price,
        decimal cost,
        ProductQuantityUnit quantityUnit = ProductQuantityUnit.Piece,
        bool isActive = false)
        => new()
        {
            Name = name,
            NameFr = nameFr,
            Slug = name.Slugify(),
            Description = description,
            BrandId = brand.Id,
            Brand = brand,
            Price = price,
            Cost = cost,
            QuantityUnit = quantityUnit,
            CategoryId = category.Id,
            Category = category,
            IsActive = isActive
        };

    public override bool Equals(object? obj) => obj is Product at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}