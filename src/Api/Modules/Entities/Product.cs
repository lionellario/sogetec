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
    public ICollection<ProductItem> Items { get; internal set; } = [];
    public ICollection<ProductImage> Images { get; internal set; } = [];
    public ICollection<ProductVariant> Specifications { get; internal set; } = [];

    public static Product Create(
        string name,
        string description,
        Brand brand,
        Category category)
        => new()
        {
            Name = name,
            Slug = name.Slugify(),
            Description = description,
            BrandId = brand.Id,
            Brand = brand,
            CategoryId = category.Id,
            Category = category,
            IsActive = true
        };

    public override bool Equals(object? obj) => obj is Product at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}