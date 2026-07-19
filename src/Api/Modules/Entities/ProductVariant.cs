using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class ProductVariant : Entity
{
    public Guid AttributeId { get; internal set; }
    public ProductAttribute? Attribute { get; internal set; }
    public string Value { get; internal set; } = default!;
    public ProductItem? Item { get; internal set; }
    public Guid ItemId { get; internal set; }

    public static ProductVariant Create(
        ProductItem item,
        ProductAttribute attribute,
        string value)
        => new()
        {
            ItemId = item.Id,
            Item = item,
            AttributeId = attribute.Id,
            Attribute = attribute,
            Value = value
        };

    public override bool Equals(object? obj) => obj is ProductVariant at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}