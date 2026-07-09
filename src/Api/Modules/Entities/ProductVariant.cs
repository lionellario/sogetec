using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class ProductVariant : Entity
{
    public string Name { get; internal set; } = default!;
    public string NameFr { get; internal set; } = default!;
    public string? Description { get; internal set; }

    public static ProductVariant Create(
        string name,
        string nameFr,
        string? description = null)
        => new()
        {
            Name = name,
            NameFr = nameFr,
            Description = description
        };

    public override bool Equals(object? obj) => obj is ProductVariant at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}
