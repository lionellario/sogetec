using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class CategoryGroup : Entity, ISortableEntity
{
    public string Name { get; internal set; } = default!;
    public string NameFr { get; internal set; } = default!;
    public string ImageUrl { get; internal set; } = default!;
    public bool IsActive { get; internal set; }
    public int SortOrder { get; set; }
    public ICollection<Category> Categories = [];

    public static CategoryGroup Create(
        string name,
        string nameFr,
        string image,
        int order = 0)
        => new()
        {
            Name = name,
            NameFr = nameFr,
            ImageUrl = image,
            IsActive = true,
            SortOrder = order
        };

    public override bool Equals(object? obj) => obj is CategoryGroup at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}
