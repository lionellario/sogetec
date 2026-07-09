using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class ProductSpecificationModel : Entity
{
    public string Name { get; internal set; } = default!;
    public string NameFr { get; internal set; } = default!;
    public ICollection<ProductSpecificationModelItem> Items { get; internal set; } = [];

    public static ProductSpecificationModel Create(
        string name,
        string nameFr)
        => new()
        {
            Name = name,
            NameFr = nameFr
        };

    public override bool Equals(object? obj) => obj is ProductSpecificationModel at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}