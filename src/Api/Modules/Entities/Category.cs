using Sogetec.Chassis.Domain;
using Sogetec.Chassis.Extensions;

namespace Api.Modules.Entities;

public sealed class Category : Entity, ISortableEntity
{
    public string Name { get; internal set; } = default!;
    public string NameFr { get; internal set; } = default!;
    public string Slug { get; internal set; } = default!;
    public int? ParentId { get; internal set; }
    public Category? Parent { get; internal set; }
    public int GroupId { get; internal set; }
    public CategoryGroup? Group { get; internal set; }
    public string? Description { get; internal set; }
    public string? ImageUrl { get; internal set; }
    public bool IsActive { get; internal set; }
    public int SortOrder { get; set; }
    public ICollection<Category> SubCategories = [];

    public static Category Create(
        string name,
        string nameFr,
        CategoryGroup group,
        Category? parent = null,
        string? description = null,
        string? image = null,
        int order = 0,
        bool isActive = false)
        => new()
        {
            Name = name,
            NameFr = nameFr,
            Slug = name.Slugify(),
            GroupId = group.Id,
            Group = group,
            ParentId = parent?.Id,
            Parent = parent,
            Description = description,
            ImageUrl = image,
            IsActive = isActive,
            SortOrder = order
        };

    public override bool Equals(object? obj) => obj is Category at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public void AssignParent(Category? parent)
    {
        if (IsAncestorOf(parent))
        {
            throw ConflictException.For<Category>(CategoryErrorCode.CategoryCannotBeSelfReferenced);
        }

        ParentId = parent?.Id;
        Parent = parent;
    }

    private bool IsAncestorOf(Category? category)
    {
        var current = category;
        while (current != null)
        {
            if (current == this) return true;
            current = current.Parent;
        }
        return false;
    }
}
