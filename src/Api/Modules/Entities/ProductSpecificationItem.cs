using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class ProductSpecificationItem : Entity
{
    public int ItemId { get; internal set; }
    public ProductSpecificationModelItem? Item { get; internal set; }
    public string Value { get; internal set; } = default!;
    public Product? Product { get; internal set; }
    public int ProductId { get; internal set; }

    public static ProductSpecificationItem Create(
        Product product,
        ProductSpecificationModelItem item,
        string value)
        => new()
        {
            ProductId = product.Id,
            Product = product,
            ItemId = item.Id,
            Item = item,
            Value = value
        };

    public override bool Equals(object? obj) => obj is ProductSpecificationItem at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}