using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class ProductAttributeHeader : Entity, ISortableEntity
{
    public string Name { get; internal set; } = default!;
    public string NameFr { get; internal set; } = default!;
    public int SortOrder { get; set; }

    public ICollection<ProductAttribute> Attributes { get; internal set; } = [];

    public static ProductAttributeHeader Create(
        string name,
        string nameFr,
        int order = 0)
        => new()
        {
            Name = name,
            NameFr = nameFr,
            SortOrder = order
        };

    public override bool Equals(object? obj) => obj is ProductAttributeHeader at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}