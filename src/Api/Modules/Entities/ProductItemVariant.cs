using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class ProductItemVariant : Entity
{
    public int ItemId { get; internal set; }
    public ProductItem? Item { get; internal set; }
    public int VariantId { get; internal set; }
    public ProductVariant? Variant { get; internal set; }

    public static ProductItemVariant Create(
       ProductItem item,
       ProductVariant variance)
        => new()
        {
            ItemId = item.Id,
            Item = item,
            VariantId = variance.Id,
            Variant = variance
        };

    public override bool Equals(object? obj) => obj is ProductItemVariant at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}