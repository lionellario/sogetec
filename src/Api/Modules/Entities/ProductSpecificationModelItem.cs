using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class ProductSpecificationModelItem : Entity
{
    public string Name { get; internal set; } = default!;
    public string NameFr { get; internal set; } = default!;
    public int HeaderId { get; internal set; }
    public ProductSpecificationModel? Header { get; internal set; }
    public bool IsVariant { get; internal set; }

    public static ProductSpecificationModelItem Create(
        ProductSpecificationModel header,
        string name,
        string nameFr,
        bool isVariant = false)
        => new()
        {
            Name = name,
            NameFr = nameFr,
            HeaderId = header.Id,
            Header = header,
            IsVariant = isVariant
        };

    public override bool Equals(object? obj) => obj is ProductSpecificationModelItem at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}
